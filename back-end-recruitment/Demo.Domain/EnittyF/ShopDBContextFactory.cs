using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Shop.Data.EntityF;
using System.IO;

namespace Demo.Domain.EnittyF
{
    class ShopDBContextFactory : IDesignTimeDbContextFactory<DbRecruitmentContext>
    {
        public DbRecruitmentContext CreateDbContext(string[] args)
        {

            var connectionString = "Data Source=LAPTOP-42SAKPS5\\SQLEXPRESS01;Initial Catalog=RecruitmentDB;User ID=tung;Password=123;Encrypt=False";

            var optionsBuilder = new DbContextOptionsBuilder<DbRecruitmentContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new DbRecruitmentContext(optionsBuilder.Options);
        }
    }
}
