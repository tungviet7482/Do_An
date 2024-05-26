using Demo.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Domain.Configuration
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("Job");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Title).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Body).IsRequired();

            //relationship
            builder.HasOne(x => x.Category).WithMany(x => x.Jobs).HasForeignKey(x => x.CategoryId);
            builder.HasOne(x => x.Location).WithMany(x => x.Jobs).HasForeignKey(x => x.LocationId);
        }
    }
}
