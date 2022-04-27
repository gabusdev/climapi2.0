using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Climapi.Api.AppServices.Swagger
{
    public static class SwaggerExtension
    {
        public static void ConfigureSwagger(this IServiceCollection services, IConfiguration config, bool jwtAuth = false)
        {
            services.AddSwaggerGen();
            services.ConfigureOptions<ConfigureSwaggerOptions>();
            services.ConfigureOptions<ConfigureSwaggerUIOptions>();

        }

        public static void UseMySwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
