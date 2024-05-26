using Demo.Application.Model;
using Demo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Interface
{
    public interface ICompanyService
    {
        Task<bool> CreateAsync(CompanyModel model);
        Task<List<CompanyModel>> GetAllAsync();
        Task<CompanyModel?> GetCompanyBySlug(string slug);
        Task<DataObject<CompanyModel>> GetPagingAsync(BaseFilter filter);
        Task<bool> UpdateAsync(int id, CompanyModel model);
    }
}
