using AutoMapper;
using Demo.Application.Interface;
using Demo.Application.Model;
using Demo.Application.Service.Base;
using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.filter;
using Demo.Domain.IRepository;
using Domain.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Service
{
    public class ContactUsService: BaseService, IContactUsService
    {
        private readonly IContactUsRepository _contactUsRepository;
        public ContactUsService(IMapper mapper, IContactUsRepository contactUsRepository) : base(mapper)
        {
            _contactUsRepository = contactUsRepository;
        }

        public async Task<bool> CreateAsync(ContactUsModel model)
        {
            var contactUs = _mapper.Map<ContactUs>(model);
            contactUs!.Processed = false;
            contactUs!.CreatedAt = DateTime.Now;
            var isSuccess = await _contactUsRepository.CreateAsync(contactUs);
            if (isSuccess)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var contactUs = await _contactUsRepository.GetByIdAsync(id);
            if (contactUs == null)
            {
                return false;
            }
            var isSuccessed = await _contactUsRepository.DeleteAsync(contactUs);
            return isSuccessed;
        }

        public Task<List<NewsModel>?> GetAllAsync() => throw new NotImplementedException();

        public async Task<DataObject<ContactUsModel>> GetPagingAsync(FilterContactUs filter)
        {
            var contactUs = await _contactUsRepository.GetPagingAsync(filter);
            var contactUsModels = _mapper.Map<List<ContactUsModel>>(contactUs.Items);
            return new DataObject<ContactUsModel>(contactUsModels, contactUs.TotalRecord);
        }

        public async Task<bool> UpdateAsync(int id, ContactUsModel model)
        {
            var contactUs = await _contactUsRepository.GetByIdAsync(id);
            if (contactUs == null)
            {
                return false;
            }

            contactUs.FullName = model.FullName;
            contactUs.Message = model.Message;
            contactUs.Processed = model.Processed;
            contactUs.Phone = model.Phone;
            contactUs.Email = model.Email;
            var isSuccessed = await _contactUsRepository.UpdateAsync(contactUs);

            return isSuccessed;
        }

        public async Task<bool> ProcessRangeAsync(List<long> ids)
        {
            return await _contactUsRepository.ProcessRangeAsync(ids);
        }
    }
}
