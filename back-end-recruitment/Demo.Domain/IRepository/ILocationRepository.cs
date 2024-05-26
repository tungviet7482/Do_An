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
    public interface ILocationRepository : IBaseRepository<Location, int>
    {
        Task<DataObject<Location>> GetPagingAsync(BaseFilter filter);
        Task<int> GetTotalLocations();
        Task<int> GetTotallocationesByKeyword(string keyword);
    }
}
