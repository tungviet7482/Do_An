using Demo.Domain.Entity;
using Demo.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using Shop.Data.EntityF;

namespace Domain.Infrastructure.Repository
{
    public class ServicePackageRepository: IServicePackageRepository
    {
        private readonly DbRecruitmentContext _db;

        public ServicePackageRepository(DbRecruitmentContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(ServicePackage entity)
        {
            await _db.ServicePackages.AddAsync(entity);
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public async Task<bool> DeleteAsync(ServicePackage entity)
        {
            entity.Deleted = true;
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public Task<bool> DeleteRangeAsync(List<ServicePackage> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ServicePackage>> GetAllAsync()
        {
            var users = await _db.ServicePackages.Where(x => !x.Deleted).ToListAsync();

            return users;
        }
        public async Task<List<ServicePackage>> GetPagingAsync(int pageIndex, int pageSize, string keyword)
        {
            var servicePackages = await _db.ServicePackages
                .AsNoTracking()
                .Where(x =>x.Deleted && (string.IsNullOrEmpty(keyword) || x.Name!.Contains(keyword)))
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return servicePackages;
        }
        public Task<ServicePackage?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<int> GetIdByNameSystem(string nameSystem)
        {
            var id = await _db.ServicePackages.Where(x => x.NameSystem == nameSystem).Select(x => x.Id).SingleAsync();
            return id;
        }
        public Task<bool> UpdateAsync(ServicePackage entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalServicePackages()
        {
            return await _db.ServicePackages.Where(x => x.Deleted).CountAsync();
        }

        public async Task<int> GetTotalServicePackagesByKeyword(string keyword)
        {
            return await _db.ServicePackages.Where(x => x.Deleted && (string.IsNullOrEmpty(keyword) || x.Name!.Contains(keyword))).CountAsync();
        }
    }
}
