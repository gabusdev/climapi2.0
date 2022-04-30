using Climapi.Core.Entities;
using Climapi.DataEF.Configurations.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Climapi.DataEF
{
    public class CoreDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public CoreDbContext(DbContextOptions option) : base(option) { }

        public DbSet<QueryRecord>? QueryRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new QueryRecordConfiguration());
        }
    }
}
