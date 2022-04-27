using Climapi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climapi.DataEF.Configurations.Entities
{
    public class QueryRecordConfiguration : IEntityTypeConfiguration<QueryRecord>
    {
        public void Configure(EntityTypeBuilder<QueryRecord> builder)
        {
            //builder.HasIndex(q => q.UserId);
            builder.HasOne(q => q.User)
                .WithMany(u => u.Queries)
                .HasForeignKey(q => q.UserId)
                .HasConstraintName("fk_user");
        }
    }
}
