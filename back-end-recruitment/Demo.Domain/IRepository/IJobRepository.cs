using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.Enum;
using Demo.Domain.filter;
using Demo.Domain.Repository.Base;

namespace Demo.Domain.IRepository
{
    public interface IJobRepository : IBaseRepository<Job, int>
    {
        Task<bool> UpdateJobUserAsync(Job_User entity);
        Task<List<Job>> GetAllDeleteAsync();
        Task<Job_User?> GetByJobUserId(int jobId, int userId);
        Task<bool> CreateJobUser(Job_User entity);
        Task<List<int>> getSavedJobIdsAsync(int id);
        Task<int> GetTotalJobsByKeyword(JobStatus jobStatus, string keyword);
        Task<Job_User> GetJobUserByCompanyIdAsync(int companyId);
        Task<Job_User> GetJobUser(int companyId);
        Task<bool> CheckDuplicateSlugJob(string slug);
        Task<DataObject<Job_User_Company>> GetPagingAsync(FilterJob filter);
        Task<List<int>> getAppliedJobIdsAsync(int id);
    }
}
