using Demo.Application.Model;
using Demo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Interface
{
    public interface ILocationService
    {
        Task<bool> CreateAsync(LocationModel model);
        Task<bool> Delete(int id);
        Task<List<LocationModel>?> GetAllAsync();
        Task<DataObject<LocationModel>> GetPagingAsync(BaseFilter page);
        Task<int> GetTotallocationes();
        Task<bool> UpdateAsync(int id, LocationModel model);
    }
}
