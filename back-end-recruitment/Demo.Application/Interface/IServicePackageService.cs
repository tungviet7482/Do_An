using Demo.Application.Model;
using Demo.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Interface
{
    public interface IServicePackageService
    {
        Task<bool> CreateAsync(ServicePackageModel model);
        Task<List<ServicePackageModel>> GetAllAsync();
        Task<bool> UpdateAsync(int id, ServicePackageModel model);
        Task<bool> Delete(int id);
    }
}
