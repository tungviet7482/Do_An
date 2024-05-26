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
    public class LocationService: BaseService,ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(IMapper mapper, ILocationRepository locationRepository) : base(mapper)
        {
            _locationRepository = locationRepository;
        }

        public async Task<bool> CreateAsync(LocationModel model)
        {
            var location = _mapper.Map<Location>(model);
            location!.CreateAt = DateTime.Now;

            var isSuccess = await _locationRepository.CreateAsync(location);
            if (isSuccess)
            {
                return true;
            }

            return false;
        }


        public async Task<bool> Delete(int id)
        {
            var location = await _locationRepository.GetByIdAsync(id);
            if (location == null)
            {
                return false;
            }
            var isSuccessed = await _locationRepository.DeleteAsync(location);
            return isSuccessed;
        }

        public async Task<int> GetTotallocationes()
        {
            return await _locationRepository.GetTotalLocations();
        }

        public async Task<List<LocationModel>?> GetAllAsync()
        {
            var locationes = await _locationRepository.GetAllAsync();
            return _mapper.Map<List<LocationModel>>(locationes);
        }

        public async Task<DataObject<LocationModel>> GetPagingAsync(BaseFilter page)
        {
            var data = await _locationRepository.GetPagingAsync(page);
            var locationesModel = _mapper.Map<List<LocationModel>>(data.Items);
            return new DataObject<LocationModel>(locationesModel, data.TotalRecord);
        }

        public async Task<bool> UpdateAsync(int id, LocationModel model)
        {
            var location = await _locationRepository.GetByIdAsync(id);
            if (location == null)
            {
                return false;
            }

            location.Name = model.Name;
            location.UpdateAt = DateTime.Now;

            var isSuccessed = await _locationRepository.UpdateAsync(location);

            return isSuccessed;
        }
    }
}
