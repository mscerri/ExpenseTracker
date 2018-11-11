using ExpenseTracker.Api.Data;
using ExpenseTracker.Api.Identity;
using ExpenseTracker.Services;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using ExpenseTracker.Api.Extensions;
using ExpenseTracker.Services.Extensions;

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
                    o.Authority = "http://localhost:24415";
                    o.SupportedTokens = SupportedTokens.Jwt;
                    o.RequireHttpsMetadata = false;
                });

            services.AddApiVersioning(options => options.AssumeDefaultVersionWhenUnspecified = true);

            var policyToScopesMappings = new Dictionary<string, string[]>
            {
                { Constants.Policy.Management, new[] { Constants.Scopes.Manage } },
                { Constants.Policy.EndUser, new[] { Constants.Scopes.TrackExpenses } },
                { Constants.Policy.Any, new [] { Constants.Scopes.Manage, Constants.Scopes.TrackExpenses } }
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
}
