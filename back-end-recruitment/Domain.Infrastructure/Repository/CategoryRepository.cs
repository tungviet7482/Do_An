using Demo.Domain.Entity;
using Demo.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using Shop.Data.EntityF;

namespace Domain.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbRecruitmentContext _db;

        public CategoryRepository(DbRecruitmentContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(Category entity)
        {
            await _db.Categories.AddAsync(entity);
            var row = await _db.SaveChangesAsync();
            return row > 0;
        }

        public async Task<int> GetTotalCategories()
        {
            return await _db.Categories.CountAsync();
        }

        public async Task<int> GetTotalCategoriesByKeyword(string keyword)
        {
            return await _db.Categories.Where(x => x.Name.Contains(keyword)).CountAsync();
        }

        public async Task<bool> DeleteAsync(Category entity)
        {
            _db.Remove(entity);
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public async Task<bool> DeleteRangeAsync(List<Category> entities)
        {
            _db.RemoveRange(entities);
            var rows = await _db.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = await _db.Categories.OrderBy(x => x.DisplayOrder).ToListAsync();
            return categories;
        }


        public async Task<List<Category_Job>> GetPagingAsync(int pageIndex, int pageSize, string keyWord)
        {
            var categoryJobs = _db.Categories
                .AsNoTracking()
                .Include(x => x.Jobs) 
                .OrderBy(x => x.DisplayOrder)
                .Select(x => new Category_Job { 
                                Id= x.Id,
                                Name = x.Name,
                                DisplayOrder = x.DisplayOrder,
                                CreateAt = x.CreateAt,
                                UpdateAt = x.UpdateAt,
                                TotalJobs = x.Jobs!.Where(x => x.Published).Count(),
                             });
            if (pageIndex > 0)
            {
                categoryJobs =  categoryJobs
                   .Where(x => string.IsNullOrEmpty(keyWord) || x.Name!.Contains(keyWord))
                   .Skip((pageIndex - 1) * pageSize)
                   .Take(pageSize);
            }

            return await categoryJobs.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            var categories = await _db.Categories.FindAsync(id);

            return categories;
        }

        public async Task<bool> UpdateAsync(Category entity)
        {
            _db.Categories.Update(entity);
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }
    }
}
