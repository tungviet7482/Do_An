using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using Shop.Data.EntityF;

namespace Domain.Infrastructure.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly DbRecruitmentContext _db;

        public LocationRepository(DbRecruitmentContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(Location entity)
        {
            await _db.AddAsync(entity);
            var row = await _db.SaveChangesAsync();
            return row > 0;
        }

        public async Task<int> GetTotalLocations()
        {
            return await _db.Locations.CountAsync();
        }

        public async Task<int> GetTotallocationesByKeyword(string keyword)
        {
            return await _db.Locations.Where(x => x.Name.Contains(keyword)).CountAsync();
        }

        public async Task<bool> DeleteAsync(Location entity)
        {
            _db.Remove(entity);
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public async Task<bool> DeleteRangeAsync(List<Location> entities)
        {
            _db.RemoveRange(entities);
            var rows = await _db.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<List<Location>> GetAllAsync()
        {
            var locationes = await _db.Locations.ToListAsync();
            return locationes;
        }


        public async Task<DataObject<Location>> GetPagingAsync(BaseFilter filter)
        {
            var locationes = _db.Locations
             .AsNoTracking()
             .Where(x => string.IsNullOrEmpty(filter.KeyWord) || x.Name!.Contains(filter.KeyWord));

        var total = await locationes.CountAsync();
            if (filter.PageIndex > 0)
            {
                locationes = locationes
                .Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize);
    }
            return new DataObject<Location>(await locationes.ToListAsync(), total) ;
        }

        public async Task<Location?> GetByIdAsync(int id)
        {
            var locationes = await _db.Locations.FindAsync(id);

            return locationes;
        }

        public async Task<bool> UpdateAsync(Location entity)
        {
            _db.Locations.Update(entity);
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }
    }
}
