using Climapi.Core.Entities;
using Climapi.Core.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Climapi.DataEF.Configurations.Entities
{
    public class RoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasData(
                new AppRole
                {
                    Name = Enum.GetName(RoleEnum.Admin),
                    NormalizedName = Enum.GetName(RoleEnum.Admin)!.ToUpper(),
                    DalyRequests = int.MaxValue,
                    WeeklyRequests = int.MaxValue,
                    MonthlyRequests = int.MaxValue,
                },
                new AppRole
                {
                    Name = Enum.GetName(RoleEnum.User),
                    NormalizedName = Enum.GetName(RoleEnum.User)!.ToUpper(),
                    DalyRequests = 20,
                    WeeklyRequests = 100,
                    MonthlyRequests = 300,
                },
                new AppRole
                {
                    Name = Enum.GetName(RoleEnum.UserPro),
                    NormalizedName = Enum.GetName(RoleEnum.UserPro)!.ToUpper(),
                    DalyRequests = 40,
                    WeeklyRequests = 200,
                    MonthlyRequests = 600,
                }
            );
        }
    }
}
