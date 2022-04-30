using Climapi.Core.Entities;
using Climapi.DataEF;
using Microsoft.AspNetCore.Identity;

namespace Climapi.Api.AppServices.Identity
{
    public static class IdentityExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            /**var builder = services.AddIdentityCore<AppUser>(q =>
            {
                q.User.RequireUniqueEmail = true;
            });
            **/
            services.AddIdentity<AppUser, AppRole>( o =>
                {
                    o.User.RequireUniqueEmail = true;
                    // o.Password.RequiredLength = 8;
                    // o.Password.RequireUppercase = false;
                    // o.Password.RequiredUniqueChars = 0;
                })
                .AddEntityFrameworkStores<CoreDbContext>()
                .AddDefaultTokenProviders();
            
            //builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            //builder.AddEntityFrameworkStores<CoreDbContext>().AddDefaultTokenProviders();
        }
    }
}
