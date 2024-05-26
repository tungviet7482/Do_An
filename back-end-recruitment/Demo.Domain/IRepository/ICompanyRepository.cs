using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.IRepository
{
    public interface ICompanyRepository : IBaseRepository<Company, int>
    {
        Task<bool> CheckDuplicateSlugCompany(string slug);
        Task<DataObject<Company>> GetPagingAsync(BaseFilter filter);
    }
}
