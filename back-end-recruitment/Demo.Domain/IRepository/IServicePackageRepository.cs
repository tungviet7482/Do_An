using Demo.Domain.Entity;
using Demo.Domain.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Shop.Data.EntityF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.IRepository
{
    public interface IServicePackageRepository : IBaseRepository<ServicePackage, int>
    {
        Task<int> GetIdByNameSystem(string nameSystem);
        Task<List<ServicePackage>> GetPagingAsync(int pageIndex, int pageSize, string keyWord);
        Task<int> GetTotalServicePackagesByKeyword(string keyword);
        Task<int> GetTotalServicePackages();
    }
}
