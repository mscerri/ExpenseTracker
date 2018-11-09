using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using ExpenseTracker.Api.Services;

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
            services.AddApiVersioning(options => options.AssumeDefaultVersionWhenUnspecified = true);

            services.AddMvcCore()
                .AddDataAnnotations()
                .AddJsonFormatters()
                .AddApiExplorer();

            services.AddCors(o => o.AddPolicy("AllowCrossOriginRequests", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddServices();

            return services.BuildServiceProvider(validateScopes: true);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("AllowCrossOriginRequests");
            //app.UseAuthentication();
            app.UseMvc();
        }
    }
}
