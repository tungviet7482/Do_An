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
    public class TransactionServicePackageConfiguration : IEntityTypeConfiguration<Transaction_ServicePackage>
    {
        public void Configure(EntityTypeBuilder<Transaction_ServicePackage> builder)
        {
            builder.ToTable("Transaction_ServicePackage");
            builder.HasKey(x => new { x.TransactionId, x.ServicePackageId });

            //Relationship
            builder.HasOne(x => x.Transaction).WithMany(x => x.Transaction_ServicePackages).HasForeignKey(x => x.TransactionId);
            builder.HasOne(x => x.ServicePackage).WithMany(x => x.Transaction_ServicePackages).HasForeignKey(x => x.ServicePackageId);
        }
    }
}
