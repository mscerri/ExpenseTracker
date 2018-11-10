using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using ExpenseTracker.Api.Data;
using ExpenseTracker.Api.Identity;
using ExpenseTracker.Api.Services;
using IdentityServer4.AccessTokenValidation;
using Microsoft.EntityFrameworkCore;

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
            services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddRequiredServices();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.Authority = Configuration.GetValue<string>("Security:Authority");
                    o.SupportedTokens = SupportedTokens.Jwt;
                });

            services.AddApiVersioning(options => options.AssumeDefaultVersionWhenUnspecified = true);

            services.AddMvcCore()
                .AddAuthorization()
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
            app.UseMvc();
        }
    }
}
