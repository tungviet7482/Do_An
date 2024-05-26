using Demo.Application.Model;
using Demo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Interface
{
    public interface ICategoryService
    {
        Task<bool> CreateAsync(CategoryModel model);
        Task<bool> Delete(int id);
        Task<List<CategoryModel>?> GetAllAsync();
        Task<DataObject<CategoryModel>> GetPagingAsync(BaseFilter page);
        Task<int> GetTotalCategories();
        Task<bool> UpdateAsync(int id, CategoryModel model);
    }
}
