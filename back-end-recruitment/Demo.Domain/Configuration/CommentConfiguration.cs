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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Advantages).IsRequired();
            builder.Property(x => x.Disadvantages).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.CompanyId).IsRequired();
            builder.Property(x => x.Rate).IsRequired();
        }
    }
}
