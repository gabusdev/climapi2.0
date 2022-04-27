using AspNetCoreRateLimit;
using Climapi.Api.AppServices.ApiVersioning;
using Climapi.Api.AppServices.Authorization;
using Climapi.Api.AppServices.Caching;
using Climapi.Api.AppServices.DI;
using Climapi.Api.AppServices.FluentValidation;
using Climapi.Api.AppServices.Identity;
using Climapi.Api.AppServices.Jwt;
using Climapi.Api.AppServices.MyCors;
using Climapi.Api.AppServices.MySqlServerContext;
using Climapi.Api.AppServices.RateLimit;
using Climapi.Api.AppServices.Swagger;
using Climapi.Api.Middlewares;
using Climapi.Services.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Climapi.Api.AppServices
{
    public static class ServiceExtensions
    {
        public static void ConfigureServiceExtensions(this IServiceCollection services, IConfiguration conf)
        {
            var conString = conf.GetConnectionString("sqlConnection");
            // Best tu use Connection String from env
            // conString = System.Environment.GetEnvironmentVariable("DbConString");

            services.ConfigureApiVersioning();
            services.ConfigureAuthorization();
            services.ConfigureCaching();
            services.ConfigureCors();
            services.ConfigureDI();
            services.ConfigureFluentValidation();
            services.ConfigureIdentity();
            services.ConfigureJwt(conf);
            services.ConfigureRateLimit();
            services.ConfigureSqlServerContext(conString);
            services.ConfigureSwagger(conf, true);
            services.AddAuthentication();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
        }

        public static void UseServiceExtensions(this IApplicationBuilder app)
        {
            app.UseMySwagger();
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseCaching();
            app.UseIpRateLimiting();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware(typeof(ErrorWrappingMiddleware));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
