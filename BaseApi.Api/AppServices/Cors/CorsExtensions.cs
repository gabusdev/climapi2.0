using Microsoft.Extensions.DependencyInjection;

namespace Climapi.Api.AppServices.MyCors
{
    public static class CorsExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                 );
            });
        }
    }
}
