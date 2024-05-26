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
    public interface IContactUsRepository : IBaseRepository<ContactUs, long>
    {
        Task<DataObject<ContactUs>> GetPagingAsync(FilterContactUs filter);
        Task<bool> ProcessRangeAsync(List<long> ids);
    }
}
