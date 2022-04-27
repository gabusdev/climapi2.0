using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Climapi.Api.AppServices.Caching
{
    public static class CachingExtension
    {
        public static void ConfigureCaching(this IServiceCollection services)
        {
            services.AddResponseCaching();
            services.AddHttpCacheHeaders(expirationOption =>
            {
                expirationOption.MaxAge = 60;
                expirationOption.CacheLocation = Marvin.Cache.Headers.CacheLocation.Private;
            },
            validationOption =>
            {
                validationOption.MustRevalidate = true;
            }
            );

        }
        public static void UseCaching(this IApplicationBuilder app)
        {
            app.UseResponseCaching();
            app.UseHttpCacheHeaders();
        }
    }
}
