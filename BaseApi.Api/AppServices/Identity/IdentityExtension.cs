using Climapi.Core.Entities;
using Climapi.DataEF;
using Microsoft.AspNetCore.Identity;

namespace Climapi.Api.AppServices.Identity
{
    public static class IdentityExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(q =>
            {
                q.User.RequireUniqueEmail = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<CoreDbContext>().AddDefaultTokenProviders();
        }
    }
}
