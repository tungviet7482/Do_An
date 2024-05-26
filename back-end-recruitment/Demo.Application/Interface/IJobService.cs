using Demo.Application.Model;
using Demo.Domain.Common;
using Demo.Domain.Enum;
using Demo.Domain.filter;

namespace Demo.Application.Interface
{
    public interface IJobService
    {
        Task<bool> CreateAsync(JobModel model);
        Task<bool> DeleteAsync(int id);
        Task<List<JobModel>> GetAllAsync();
        Task<JobModel?> GetJobBySlug(string slug);
        Task<DataObject<JobModel>> GetPagingAsync(FilterJob filter);
        Task<string?> GetUrlAsync(int id);
        Task<bool> UpdateAsync(int id, JobModel model);
    }
}
