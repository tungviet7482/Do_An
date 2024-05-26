using Demo.Domain.Entity;
using Demo.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using Shop.Data.EntityF;

namespace Domain.Infrastructure.Repository
{
    public class Transaction_ServicePackageRepository : ITransaction_ServicePackageRepository
    {
        private readonly DbRecruitmentContext _db;

        public Transaction_ServicePackageRepository(DbRecruitmentContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(Transaction_ServicePackage entity)
        {
            await _db.Transaction_ServicePackages.AddAsync(entity);
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public async Task<bool> DeleteAsync(Transaction_ServicePackage entity)
        {
            _db.Remove(entity);
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public Task<bool> DeleteRangeAsync(List<Transaction_ServicePackage> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Transaction_ServicePackage>> GetAllAsync()
        {
            var users = await _db.Transaction_ServicePackages.ToListAsync();

            return users;
        }

        public async Task<Transaction_ServicePackage?> GetByIdAsync(int id)
        {
            var tsp = await _db.Transaction_ServicePackages.FindAsync(id);

            return tsp;
        }

        public async Task<bool> UpdateAsync(Transaction_ServicePackage entity)
        {
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }
    }
}
