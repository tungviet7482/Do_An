using Demo.Domain.Configuration;
using Demo.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Shop.Data.EntityF
{
    public class DbRecruitmentContext : IdentityDbContext<User, Role, int>
    {
        public DbRecruitmentContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new JobConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new MediaFolderConfiguration());
            modelBuilder.ApplyConfiguration(new MediaFileConfiguration());
            modelBuilder.ApplyConfiguration(new ServicePackageConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new NewsConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new ContactUsConfiguration());
            modelBuilder.ApplyConfiguration(new JobUserConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionServicePackageConfiguration());


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName("" + tableName.Substring(6));
                }
            }
            //create data
            //modelBuilder.Seed();
        }

        public DbSet<Category> Categories { set; get; }
        public DbSet<Company> Companies { set; get; }
        public DbSet<Job> Jobs { set; get; }
        public DbSet<News> News { set; get; }
        public DbSet<Comment> Comments { set; get; }
        public DbSet<ContactUs> ContactUs { set; get; }
        public DbSet<Job_User> Job_Users { set; get; }
        public DbSet<Location> Locations { set; get; }
        public DbSet<MediaFile> MediaFiles { set; get; }
        public DbSet<MediaFolder> MediaFolders { set; get; }
        public DbSet<ServicePackage> ServicePackages { set; get; }
        public DbSet<Transaction> Transactions { set; get; }
        public DbSet<Transaction_ServicePackage> Transaction_ServicePackages { set; get; }
    }
}
