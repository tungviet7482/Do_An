using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.filter;
using Demo.Domain.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.IRepository
{
    public interface IUserRepository : IBaseRepository<User, int>
    {
        Task<DataObject<Job_User>> GetCandidatesAppliedPagingAsync(FilterCandidate filter);
        Task<DataObject<User>> GetCandidatesPagingAsync(FilterCandidate filter);
    }
}
