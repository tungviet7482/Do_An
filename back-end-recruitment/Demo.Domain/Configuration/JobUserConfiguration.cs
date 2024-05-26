using Demo.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Configuration
{
    public class JobUserConfiguration : IEntityTypeConfiguration<Job_User>
    {
        public void Configure(EntityTypeBuilder<Job_User> builder)
        {
            builder.ToTable("Job_User");
            builder.HasKey(x => new { x.JobId, x.UserId });

            //Relationship
            builder.HasOne(x => x.Job).WithMany(x => x.Job_Users).HasForeignKey(x => x.JobId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.User).WithMany(x => x.Job_Users).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            
        }
    }
}
