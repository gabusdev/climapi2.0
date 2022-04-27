using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Climapi.Api.AppServices.ApiVersioning
{
    public static class ApiVersioningExtension
    {
        public static void ConfigureApiVersioning(this IServiceCollection services, bool headerCheck = true)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                if (headerCheck)
                    opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v' VVV";
            });
        }
    }
}
