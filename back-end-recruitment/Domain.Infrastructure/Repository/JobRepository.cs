using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.Enum;
using Demo.Domain.filter;
using Demo.Domain.IRepository;
using Domain.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shop.Data.EntityF;

namespace Domain.Infrastructure.Repository
{
    public class JobRepository : IJobRepository
    {
        private readonly DbRecruitmentContext _db;
        private readonly FilterHandler _filterHandler;

        public JobRepository(DbRecruitmentContext db, FilterHandler filterHandler)
        {
            _db = db;
            _filterHandler = filterHandler;
        }

        public async Task<List<Job>> GetAllAsync()
        {
            var jobs = await _db.Jobs.Where(x => !x.Deleted).ToListAsync();
            return jobs;
        }

        public async Task<DataObject<Job_User_Company>> GetPagingAsync(FilterJob filter)
        {
            var jobs = (from j in _db.Jobs
                        join l in _db.Locations
                        on j.LocationId equals l.Id
                        join ju in _db.Job_Users
                        on j.Id equals ju.JobId
                        join u in _db.Users
                        on ju.UserId equals u.Id
                        join c in _db.Companies
                        on u.CompanyId equals c.Id
                        where !j.Deleted &&
                              !u.Deleted &&
                              (string.IsNullOrEmpty(filter.KeyWord) || j.Title!.Contains(filter.KeyWord))
                        orderby j.CreateAt descending   
                        select new Job_User_Company
                        {
                            Id = j.Id,
                            Title = j.Title,
                            Body = j.Body,
                            ShortDescription = j.ShortDescription,
                            Quantity = j.Quantity,
                            MinSalary = j.MinSalary,
                            MaxSalary = j.MaxSalary,
                            WorkExperience = j.WorkExperience,
                            Address = j.Address,
                            Published = j.Published,
                            StartDate = j.StartDate,
                            EndDate = j.EndDate,
                            CategoryId = j.CategoryId,
                            LocationId = j.LocationId,
                            LocationName = l.Name,
                            UserId = ju.UserId,
                            FullName = u.FullName,
                            PhoneNumber = u.PhoneNumber,
                            Dob = u.Dob,
                            CompanyId = u.CompanyId,
                            CompanyName = c.Name,
                            CompanyDescription = c.Description,
                            CompanyEmployeeCount = c.EmployeeCount,
                            CompanyWebsiteUrl = c.WebsiteUrl,
                            CompanyAddress = c.Address,
                            CompanyImageId = c.ImageId,
                            CompanyPreviewImageId = c.PreviewImageId,
                            JobUserUPdateAt = ju.UpdateAt,
                            JobType = j.JobType,
                            Slug = j.Slug,
                            CompanySlug = c.Slug
                        });

            jobs = _filterHandler.HandleFilterJob(jobs, filter);
            var total = await jobs.CountAsync();
            if (filter.PageIndex > 0)
            {
                jobs = jobs
                .Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize);
            }
            return new DataObject<Job_User_Company>( await jobs.ToListAsync(), total) ;
        }

        public async Task<int> GetTotalJobsByKeyword(JobStatus jobStatus, string keyword)
        {
            var now = DateTime.Now;
            if (jobStatus == JobStatus.ACTIVE)
            {
                return await _db.Jobs.Where(x => x.EndDate > now && keyword.IsNullOrEmpty() ? true : x.Title.Contains(keyword)).CountAsync();
            }
            else if (jobStatus == JobStatus.EXPIRED)
            {
                return await _db.Jobs.Where(x => x.EndDate < now && keyword.IsNullOrEmpty() ? true : x.Title.Contains(keyword)).CountAsync();
            }
            return 0;
        }


        public async Task<List<Job>> GetAllDeleteAsync()
        {
            var jobs = await _db.Jobs.Where(x => x.Deleted).ToListAsync();
            return jobs;
        }

        public async Task<List<int>> getSavedJobIdsAsync(int id)
        {
            var jobIds = from ju in _db.Job_Users
                       where ju.UserId == id && ju.FollowedJob == true
                       select ju.JobId;

            return await jobIds.ToListAsync();
        }

        public async Task<List<int>> getAppliedJobIdsAsync(int id)
        {
            var jobIds = from ju in _db.Job_Users
                         where ju.UserId == id && ju.Applied == true
                         select ju.JobId;

            return await jobIds.ToListAsync();
        }

        public async Task<Job_User> GetJobUserByCompanyIdAsync(int companyId)
        {
            var jobUser = await (from j in _db.Jobs
                                 join ju in _db.Job_Users
                                 on j.Id equals ju.JobId
                                 join u in _db.Users
                                 on ju.UserId equals u.Id
                                 where u.CompanyId == companyId
                                 select ju).FirstOrDefaultAsync();

            return jobUser;
        }

        public async Task<Job_User> GetJobUser(int companyId)
        {
            var jobUser = await (from j in _db.Jobs
                                 join ju in _db.Job_Users
                                 on j.Id equals ju.JobId
                                 join u in _db.Users
                                 on ju.UserId equals u.Id
                                 where u.CompanyId != null
                                 select ju).FirstOrDefaultAsync();

            return jobUser;
        }

        public async Task<Job_User?> GetByJobUserId(int jobId, int userId)
        {
            return await _db.Job_Users.Where(x => x.JobId == jobId && x.UserId == userId).FirstOrDefaultAsync();
        }
       
        public async Task<Job?> GetByIdAsync(int id)
        {
            var job = await _db.Jobs.FindAsync(id);
            if (job != null)
            {
                return job.Deleted ? null : job;
            }
            return job;
        }

        public async Task<bool> CreateAsync(Job entity)
        {

            _db.Jobs.Add(entity);
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public async Task<bool> CreateJobUser(Job_User entity)
        {
            await _db.Job_Users.AddAsync(entity);
            var row = await _db.SaveChangesAsync();
            return row > 0;
        }

        public async Task<bool> UpdateJobUserAsync(Job_User entity)
        {
            var row = await _db.SaveChangesAsync();
            return row > 0;
        }

        public async Task<bool> UpdateAsync(Job entity)
        {
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public async Task<bool> DeleteAsync(Job entity)
        {
            entity.Deleted = true;

            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public async Task<bool> DeleteRangeAsync(List<Job> entities)
        {
            foreach (var entity in entities)
            {
                entity.Deleted = true;
            }
            var rows = await _db.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> CheckDuplicateSlugJob(string slug)
        {
            var company = await _db.Jobs.AsNoTracking().Where(x => x.Slug == slug).FirstOrDefaultAsync();
            return company != null;
        }
    }
}
