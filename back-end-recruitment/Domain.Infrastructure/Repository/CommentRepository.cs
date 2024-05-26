using Demo.Domain.Common;
using Demo.Domain.DataObejct;
using Demo.Domain.Entity;
using Demo.Domain.IRepository;
using Domain.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Shop.Data.EntityF;

namespace Domain.Infrastructure.Repository
{
    public class CommentRepository : ICommentRepository
    {   
        private readonly DbRecruitmentContext _db;
        private readonly FilterHandler _filterHandler;

        public CommentRepository(DbRecruitmentContext db, FilterHandler filterHandler)
        {
            _db = db;
            _filterHandler = filterHandler;
        }

        public async Task<bool> CreateAsync(Comment entity)
        {
            var comment = _db.Comments.Where(x => x.UserId == entity.UserId && x.CompanyId == entity.CompanyId).FirstOrDefault();
            if(comment != null)
            {
                throw new Exception("Bạn đã đánh giá công ty rồi");
            }    
            await _db.Comments.AddAsync(entity);
            var row = await _db.SaveChangesAsync();
            return row > 0;
        }

        public async Task<bool> DeleteAsync(Comment entity)
        {
            _db.Comments.Remove(entity);
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public Task<bool> DeleteRangeAsync(List<Comment> entities)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Comment?> GetByIdAsync(long id)
        {
            var news = await _db.Comments.FindAsync(id);

            return news;
        }

        public async Task<bool> UpdateAsync(Comment entity)
        {
            _db.Comments.Update(entity);
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }
        public async Task<DataComment<Comment>> GetPagingAsync(BaseFilter filter)
        {
            var comments = _db.Comments
                .AsNoTracking()
                .OrderBy(x => x.CreatedAt);
                
            if(filter.Published != null)
            {
                comments = (IOrderedQueryable<Comment>) comments.Where(x => x.IsPublish == filter.Published);
            }

            if(filter.CompanyId != null)
            {
                comments = (IOrderedQueryable<Comment>)comments.Where(x => x.CompanyId == filter.CompanyId);
            }
            var starRating = new int[5];
            for(var i = 1; i < 6; i++)
            {
                starRating[i - 1] = comments.Where(x => x.Rate == i).Count();
            }
            var sumStar = comments.Select(x => x.Rate).Sum();
            var total = await comments.CountAsync();
            if (filter.PageIndex > 0)
            {
                comments = (IOrderedQueryable<Comment>)comments
                .Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize);
            }
            return new DataComment<Comment>(await comments.ToListAsync(), total, starRating, total == 0? 0 : float.Parse(sumStar.ToString())/total);
        }
    }
}
