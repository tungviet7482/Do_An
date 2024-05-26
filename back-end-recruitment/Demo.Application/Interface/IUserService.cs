using Demo.Application.Model;
using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.filter;

namespace Demo.Application.Interface
{
    public interface IUserService
    {
        Task<bool> AssignRoleToUser(int id, string typeRole);
        Task<string> Authenticate(UserModel model);
        Task<bool> Delete(int id);
        Task<bool?> SaveJobAsync(Job_UserModel model);
        Task<DataObject<JobModel>> getSavedJobsPagingAsync(int id, FilterJob page);
        Task<DataObject<UserModel>> GetPagingAsync(string roleName, FilterUser page);
        Task<List<string>?> GetRolesAsync(UserModel model);
        Task<UserModel?> GetUserByIdAsync(int id);
        Task<bool> Register(UserModel model);
        Task<bool> Update(int id, UserModel model);
        Task<bool> ApplyJob(Job_UserModel model);
        Task<DataObject<JobModel>> getAppliedJobsPagingAsync(int id, FilterJob filter);
        Task<DataObject<UserModel>> GetCandidatesAppliedPaging(FilterCandidate filter);
        Task<bool> UpdateJobUser(Job_UserModel model);
        Task<DataObject<UserModel>> GetCandidatesPaging(FilterCandidate filter);
    }
}
