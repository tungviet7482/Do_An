using Demo.Domain.Entity;
using Demo.Domain.Enum;
using Demo.Domain.filter;

namespace Domain.Infrastructure.Common
{
    public class FilterHandler
    {
        public IQueryable<Job_User_Company> HandleFilterJob(IQueryable<Job_User_Company> jobs, FilterJob filter)
        {
            var now = DateTime.Now;
            if (filter.JobId != null)
            {
                jobs = jobs.Where(x => x.Id == filter.JobId);
            }
            if (filter.Published != null)
            {
                jobs = jobs.Where(x => x.Published == filter.Published);
            }
            if (filter.CompanyId != null)
            {
                jobs = jobs.Where(x => x.CompanyId == filter.CompanyId);
            }
            if (filter.Slug != null)
            {
                jobs = jobs.Where(x => x.Slug.Equals(filter.Slug));
            }
            if (filter.JobIds != null)
            {
                jobs = jobs
                    .Where(x => filter.JobIds.Contains(x.Id))
                    .OrderByDescending(x => x.JobUserUPdateAt);
            }
            if (filter.JobType != null)
            {
                jobs = jobs.Where(x => x.JobType == filter.JobType);
            }
            if (filter.CategoryId != null)
            {
                jobs = jobs.Where(x => x.CategoryId == filter.CategoryId);
            }
            if (filter.LocationId != null)
            {
                jobs = jobs.Where(x => x.LocationId == filter.LocationId);
            }
            if (filter.WorkExperience != null)
            {
                if (filter.WorkExperience == 0)
                {
                    jobs = jobs.Where(x => x.WorkExperience == 0);
                }
                if (filter.WorkExperience < 6)
                {
                    jobs = jobs.Where(x => x.WorkExperience >= filter.WorkExperience - 1 && x.WorkExperience < filter.WorkExperience);
                }
                else
                {
                    jobs = jobs.Where(x => x.WorkExperience >= filter.WorkExperience - 1);
                }
            }
            if (filter.Salary != null)
            {

                switch (filter.Salary)
                {
                    case 1:
                        jobs = jobs.Where(x => x.MinSalary < 10 && x.MinSalary != 0);
                        break;
                    case 2:
                        jobs = jobs.Where(x => !(x.MaxSalary < 10 || x.MinSalary >= 20));
                        break;
                    case 3:
                        jobs = jobs.Where(x => !(x.MaxSalary < 20 || x.MinSalary >= 30));
                        break;
                    case 4:
                        jobs = jobs.Where(x => !(x.MaxSalary < 30 || x.MinSalary >= 40));
                        break;
                    case 5:
                        jobs = jobs.Where(x => !(x.MaxSalary < 40 || x.MinSalary >= 50));
                        break;
                    case 6:
                        jobs = jobs.Where(x => !(x.MaxSalary < 50));
                        break;
                }
            }
            if (filter.JobStatus != null)
            {
                if (filter.JobStatus == JobStatus.ACTIVE)
                {
                    jobs = jobs
                    .Where(x => x.EndDate > now);
                }
                else if (filter.JobStatus == JobStatus.EXPIRED)
                {
                    jobs = jobs
                        .Where(x => x.EndDate < now);
                }

            }

            return jobs;
        }

        public IQueryable<News> HandleFilterNews(IQueryable<News> news, FilterNews filter)
        {
            if (filter.NewsId != null)
            {
                news = news.Where(x => x.Id == filter.NewsId);
            }
            if (filter.Published != null)
            {
                news = news.Where(x => x.IsPublished == filter.Published);
            }
            if (filter.Slug != null)
            {
                news = news.Where(x => x.Slug.Equals(filter.Slug));
            }

            return news;
        }

        public IQueryable<Job_User> HandleFilterCandidateApplied(IQueryable<Job_User> jobUsers, FilterCandidate filter)
        {
            if(filter.JobId != null)
            {
                jobUsers = jobUsers.Where(x => x.JobId == filter.JobId);
            }
            if (filter.Applied != null)
            {
                jobUsers = jobUsers.Where(x => x.Applied == filter.Applied);
            }
            if (filter.Viewed)
            {
                jobUsers = jobUsers.Where(x => x.Viewed && x.Interested == filter.Interested);
            }
            else
            {
                jobUsers = jobUsers.Where(x => !x.Viewed);
            }

            return jobUsers;
        }

        public IEnumerable<User>? HandleFilterCandidate(IEnumerable<User>? users, FilterCandidate filter)
        {

            if (filter.LocationId != null)
            {
                users = users?.Where(x => x.LocationId == filter.LocationId);
            }
            if (filter.FieldId != null)
            {
                users = users?.Where(x => x.FieldId == filter.FieldId);
            }
            if(filter.MinAge != null)
            {
                users = users?.Where(x => x.Dob.Year >= filter.MinAge);
            }
            if (filter.MaxAge != null)
            {
                users = users?.Where(x => x.Dob.Year <= filter.MaxAge);
            }

            return users;
        }
    }
}
