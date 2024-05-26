using AutoMapper;
using Demo.Application.Interface;
using Demo.Application.Model;
using Demo.Application.Service.Base;
using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Service
{
    public class ServicePackageService : BaseService, IServicePackageService
    {
        private readonly IServicePackageRepository _servicePackageRepository;

        public ServicePackageService(IMapper mapper, IServicePackageRepository servicePackageRepository) : base(mapper)
        {
            _servicePackageRepository = servicePackageRepository;
        }

        public async Task<bool> CreateAsync(ServicePackageModel model)
        {
            var servicePackage = _mapper.Map<ServicePackage>(model);
            var isSuccess = await _servicePackageRepository.CreateAsync(servicePackage);
            if (isSuccess)
            {
                return true;
            }

            return false;
        }

        public async Task<DataObject<ServicePackageModel>> GetPagingAsync(BaseFilter page)
        {
            var total = 0;
            var locationes = await _servicePackageRepository.GetPagingAsync(page.PageIndex, page.PageSize, page.KeyWord);
            var servicePackageModels = _mapper.Map<List<ServicePackageModel>>(locationes);
            if (page.KeyWord == null)
            {
                total = await _servicePackageRepository.GetTotalServicePackages();
            }
            else
            {
                total = await _servicePackageRepository.GetTotalServicePackagesByKeyword(page.KeyWord);
            }
            return new DataObject<ServicePackageModel>(servicePackageModels, total);
        }

        public async Task<bool> Delete(int id)
        {
            var servicePackage = await _servicePackageRepository.GetByIdAsync(id);
            var isSuccessed = await _servicePackageRepository.DeleteAsync(servicePackage);

            return isSuccessed;
        }

        public async Task<List<ServicePackageModel>?> GetAllAsync()
        {
            var servicePkages = await _servicePackageRepository.GetAllAsync();
            return _mapper.Map<List<ServicePackageModel>>(servicePkages);
        }

        public Task<bool> UpdateAsync(int id, ServicePackageModel model)
        {
            throw new NotImplementedException();
        }
    }
}
