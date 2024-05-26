using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using Shop.Data.EntityF;

namespace Domain.Infrastructure.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DbRecruitmentContext _db;

        public CompanyRepository(DbRecruitmentContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(Company entity)
        {

            _db.Companies.Add(entity);
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public async Task<bool> DeleteAsync(Company entity)
        {
            entity.Deleted = true;

            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public async Task<bool> DeleteRangeAsync(List<Company> entities)
        {
            foreach (var entity in entities)
            {
                entity.Deleted = true;
            }
            var rows = await _db.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<List<Company>> GetAllAsync()
        {
            var companies = await _db.Companies.Where(x => !x.Deleted).ToListAsync();

            return companies;
        }

        public async Task<DataObject<Company>> GetPagingAsync(BaseFilter filter)
        {
            var companies = _db.Companies
                .AsNoTracking()
                .Include(x => x.User)
                .Where(x =>
                !x.Deleted &&
                (string.IsNullOrEmpty(filter.Slug) || x.Slug == filter.Slug) &&
                (string.IsNullOrEmpty(filter.KeyWord) || x.Name.Contains(filter.KeyWord)));
            var total = await companies.CountAsync();
            if (filter.PageIndex > 0)
            {
                companies = companies
                    .Skip((filter.PageIndex - 1) * filter.PageSize)
                    .Take(filter.PageSize);
            }

            return new DataObject<Company>(await companies.ToListAsync(), total);
        }

        public async Task<List<Company>> GetAllDeleteAsync()
        {
            var companies = await _db.Companies.Where(x => x.Deleted).ToListAsync();
            return companies;
        }

        public async Task<Company?> GetByIdAsync(int id)
        {
            var company = await _db.Companies.FindAsync(id);
            if (company == null)
            {
                return null;
            }
            return company.Deleted ? null : company;
        }

        public async Task<bool> UpdateAsync(Company entity)
        {
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }
        public async Task<bool> CheckDuplicateSlugCompany(string slug)
        {
            var company = await _db.Companies.AsNoTracking().Where(x => x.Slug == slug).FirstOrDefaultAsync();
            return company != null;
        }
    }
}
