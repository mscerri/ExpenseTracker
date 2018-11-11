using ExpenseTracker.Api.Data;
using ExpenseTracker.Api.Identity;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using ExpenseTracker.Services;
using ExpenseTracker.Services.Exceptions;

namespace ExpenseTracker.Api
{
    public class Startup : IStartup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryIdentityResources(InMemoryConfigs.GetIdentityResources())
                .AddInMemoryApiResources(InMemoryConfigs.GetApiResources())
                .AddInMemoryClients(InMemoryConfigs.GetClients())
                .AddDeveloperSigningCredential() //not to be used on prod
                .AddRequiredServices();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.Authority = Configuration.GetValue<string>("Security:Authority");
                    o.SupportedTokens = SupportedTokens.Jwt;
                });

            services.AddApiVersioning(options => options.AssumeDefaultVersionWhenUnspecified = true);

            var policyToScopesMappings = new Dictionary<string, string[]>
            {
                { Constants.Policy.Management, new[] { Constants.Scopes.Manage } },
                { Constants.Policy.EndUser, new[] { Constants.Scopes.TrackExpenses } }
            };

            services.AddMvcCore()
                .AddAuthorization(options =>
                {
                    foreach (var policyToScopesMapping in policyToScopesMappings)
                    {
                        options.AddPolicy(policyToScopesMapping.Key, policy =>
                        {
                            policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                .RequireAuthenticatedUser().RequireClaim("scope", policyToScopesMapping.Value);
                        });
                    }
                })
                .AddDataAnnotations()
                .AddJsonFormatters()
                .AddApiExplorer();

            //services.AddCors(o => o.AddPolicy("AllowCrossOriginRequests", builder =>
            //{
            //    builder
            //        .AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader();
            //}));

            services.AddDbContext<ExpenseTrackerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ExpenseTrackerDatabase")));

            services.AddServices();

            return services.BuildServiceProvider(validateScopes: true);
        }

        public void Configure(IApplicationBuilder app)
        {
            //app.UseCors("AllowCrossOriginRequests");
            app.UseIdentityServer();
            app.UseExpenseTrackerExceptionHandler();
            app.UseMvc();
        }
    }

    public static class ApplicationBuilderExtensions
    {
        public static void UseExpenseTrackerExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;


                    if (exception is BadHttpRequestException badHttpRequestException)
                    {
                        //Log.Warning(errorFeature.Error, "Bad request registered - delegating to Kestrel to handle.");
                        throw badHttpRequestException;
                    }

                    var problemDetails = new ProblemDetails
                    {
                        Title = "An unexpected error occurred",
                        Status = (int) HttpStatusCode.InternalServerError,
                        Detail = "An unexpected error occurred. The instance value will be helpful to debug the problem",
                        Instance = Guid.NewGuid().ToString(),
                        //problemDetails.Title = "Invalid Request";
                        //problemDetails.Status = (int)ExtractStatusCodeFromBadHttpRequestException(badHttpRequestException, problemDetails.Instance);
                        //problemDetails.Detail = badHttpRequestException.Message;
                    };

                    //Log.Error(errorFeature.Error, "An exception has been caught, error id = {errorId}.", metadata.ErrorId);

                    if (exception is ServiceException serviceEx)
                    {
                        switch (serviceEx)
                        {
                            case ConflictException conflictException:
                                problemDetails.Title = "A conf";
                                problemDetails.Status = (int)HttpStatusCode.Conflict;
                                break;
                            case NotFoundException notFoundException:
                                problemDetails.Title = "Resource not found";
                                problemDetails.Status = (int)HttpStatusCode.NotFound;
                                problemDetails.Detail = notFoundException.Message;
                                break;
                        }
                    }

                    context.Response.ContentType = "application/problem+json";
                    context.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
                });
            });
        }
    }
}
