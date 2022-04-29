using Climapi.Services;
using Climapi.Services.Impl;
using DataEF.UnitOfWork;

namespace Climapi.Api.AppServices.DI
{
    public static class DIExtensions
    {
        public static void ConfigureDI(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthManagerService, AuthManagerService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
