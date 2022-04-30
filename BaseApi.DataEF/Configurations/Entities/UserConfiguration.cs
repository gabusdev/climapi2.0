using Climapi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Climapi.DataEF.Configurations.Entities
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            //builder.HasMany<QueryRecord>();
            builder.HasMany(u => u.UserAppRoles)
                .WithMany(a => a.AppUsers);
        }
    }
}
