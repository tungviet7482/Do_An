using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.filter;
using Demo.Domain.IRepository;
using Domain.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Shop.Data.EntityF;

namespace Domain.Infrastructure.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly DbRecruitmentContext _db;
        private readonly FilterHandler _filterHandler;

        public UserRepository(DbRecruitmentContext db, FilterHandler filterHandler)
        {
            _db = db;
            _filterHandler = filterHandler;
        }

        public Task<bool> CreateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRangeAsync(List<User> entities)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<DataObject<Job_User>> GetCandidatesAppliedPagingAsync(FilterCandidate filter)
        {
            var jobUsers = _db.Job_Users
                .AsNoTracking()
                .Include(x => x.User)
                .OrderBy(x => x.UpdateAt)
                .Where(x => !x.User.Deleted &&
                            x.User.CompanyId == null &&
                            (string.IsNullOrEmpty(filter.KeyWord) || x.User.Technology!.Contains(filter.KeyWord)));

            jobUsers = _filterHandler.HandleFilterCandidateApplied(jobUsers, filter);
            var total = await jobUsers.CountAsync();
            if (filter.PageIndex > 0)
            {
                jobUsers = jobUsers
                .Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize);
            }
            return new DataObject<Job_User>(await jobUsers.ToListAsync(), total);
        }

        public async Task<DataObject<User>> GetCandidatesPagingAsync(FilterCandidate filter)
        {
            var users = _db.Users
                .AsNoTracking()
                .Where(x => !x.Deleted &&
                            x.CompanyId == null &&
                            (string.IsNullOrEmpty(filter.KeyWord) || x.Technology!.Contains(filter.KeyWord)));

            var total = await users.CountAsync();
            if (filter.PageIndex > 0)
            {
                users = users
                .Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize);
            }
            return new DataObject<User>(await users.ToListAsync(), total);
        }

        public Task<bool> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
