//using Demo.Domain.Entity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Demo.Domain.Configuration
//{
//    public class JobCategoryConfiguration : IEntityTypeConfiguration<Job_Category>
//    {
//        public void Configure(EntityTypeBuilder<Job_Category> builder)
//        {
//            builder.ToTable("Job_Category");
//            builder.HasKey(x => new { x.JobId, x.CategoryId } );
            
//            //Relationship
//            builder.HasOne(x => x.Job).WithMany(x => x.Job_Categories).HasForeignKey(x => x.JobId);
//            builder.HasOne(x => x.Category).WithMany(x => x.Job_Categories).HasForeignKey(x => x.CategoryId);
//        }
//    }
//}
