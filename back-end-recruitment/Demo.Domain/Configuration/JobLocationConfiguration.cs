//using Demo.Domain.Entity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Demo.Domain.Configuration
//{
//    public class JobLocationConfiguration : IEntityTypeConfiguration<Job_Location>
//    {
//        public void Configure(EntityTypeBuilder<Job_Location> builder)
//        {
//            builder.ToTable("Job_Location");
//            builder.HasKey(x => new { x.JobId, x.LocationId });

//            //Relationship
//            builder.HasOne(x => x.Job).WithMany(x => x.Job_Locations).HasForeignKey(x => x.JobId);
//            builder.HasOne(x => x.Location).WithMany(x => x.Job_Locations).HasForeignKey(x => x.LocationId);
//        }
//    }
//}
