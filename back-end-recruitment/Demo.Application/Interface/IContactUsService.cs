using Demo.Application.Model;
using Demo.Domain.Common;
using Demo.Domain.filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Interface
{
    public interface IContactUsService
    {
        Task<bool> CreateAsync(ContactUsModel model);
        Task<bool> Delete(int id);
        Task<DataObject<ContactUsModel>> GetPagingAsync(FilterContactUs filter);
        Task<bool> ProcessRangeAsync(List<long> ids);
        Task<bool> UpdateAsync(int id, ContactUsModel model);
    }
}
