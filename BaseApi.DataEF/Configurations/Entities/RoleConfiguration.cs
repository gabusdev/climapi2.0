using Climapi.Core.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Climapi.DataEF.Configurations.Entities
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = Enum.GetName(RoleEnum.Admin),
                    NormalizedName = Enum.GetName(RoleEnum.Admin)!.ToUpper()
                },
                new IdentityRole
                {
                    Name = Enum.GetName(RoleEnum.User),
                    NormalizedName = Enum.GetName(RoleEnum.User)!.ToUpper()
                }
            );
        }
    }
}
