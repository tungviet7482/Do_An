using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.filter;
using Demo.Domain.IRepository;
using Domain.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Shop.Data.EntityF;

namespace Domain.Infrastructure.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly DbRecruitmentContext _db;
        private readonly FilterHandler _filterHandler;

        public NewsRepository(DbRecruitmentContext db, FilterHandler filterHandler)
        {
            _db = db;
            _filterHandler = filterHandler;
        }

        public async Task<bool> CreateAsync(News entity)
        {
            await _db.News.AddAsync(entity);
            var row = await _db.SaveChangesAsync();
            return row > 0;
        }

        public async Task<bool> DeleteAsync(News entity)
        {
            entity.Deleted = true;
            _db.Update(entity);
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public Task<bool> DeleteRangeAsync(List<News> entities)
        {
            throw new NotImplementedException();
        }

        public Task<List<News>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<News?> GetByIdAsync(int id)
        {
            var news = await _db.News.FindAsync(id);

            return news;
        }

        public async Task<bool> UpdateAsync(News entity)
        {
            _db.News.Update(entity);
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public async Task<DataObject<News>> GetPagingAsync(FilterNews filter)
        {
            var news = _db.News
                .AsNoTracking()
                .OrderBy(x => x.DisplayOrder)
                .Where(x => !x.Deleted &&
                              (string.IsNullOrEmpty(filter.KeyWord) || x.Title.Contains(filter.KeyWord)));


            news = _filterHandler.HandleFilterNews(news, filter);
            var total = await news.CountAsync();
            if (filter.PageIndex > 0)
            {
                news = news
                .Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize);
            }
            return new DataObject<News>(await news.ToListAsync(), total);
        }

        public async Task<bool> CheckDuplicateSlugNews(string slug)
        {
            var company = await _db.Companies.AsNoTracking().Where(x => x.Slug == slug).FirstOrDefaultAsync();
            return company != null;
        }
    }
}
