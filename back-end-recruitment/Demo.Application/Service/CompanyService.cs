using AutoMapper;
using Demo.Application.Interface;
using Demo.Application.Model;
using Demo.Application.Service.Base;
using Demo.Domain.Common;
using Demo.Domain.IRepository;

namespace Demo.Application.Service
{
    public class CompanyService : BaseService, ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMediaService _mediaService;

        public CompanyService(IMapper mapper, ICompanyRepository companyRepository, IMediaService mediaService) : base(mapper)
        {
            _companyRepository = companyRepository;
            _mediaService = mediaService;
        }

        public Task<bool> CreateAsync(CompanyModel model)
        {
            throw new NotImplementedException();
        }

        public Task<List<CompanyModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<DataObject<CompanyModel>> GetPagingAsync(BaseFilter filter)
        {
            var dataCompanies = await _companyRepository.GetPagingAsync(filter);
            var companiesModel = _mapper.Map<List<CompanyModel>>(dataCompanies.Items);
            foreach (var company in companiesModel)
            {
                company.PreviewImgUrl = await _mediaService.GetUrlAsync(company.PreviewImageId ?? 0);
                company.ImgUrl = await _mediaService.GetUrlAsync(company.ImageId ?? 0);

            }

            return new DataObject<CompanyModel>(companiesModel, dataCompanies.TotalRecord);
        }

        public async Task<CompanyModel?> GetCompanyBySlug(string slug)
        {
            var filter = new BaseFilter
            {
                Slug = slug,
                Published = true
            };
            var companies = await _companyRepository.GetPagingAsync(filter);
            var companyModel = _mapper.Map<List<CompanyModel>>(companies.Items)!.FirstOrDefault();
            if(companyModel == null)
            {
                return null;
            }
            companyModel!.PreviewImgUrl = await _mediaService.GetUrlAsync(companyModel.PreviewImageId ?? 0);
            companyModel.ImgUrl = await _mediaService.GetUrlAsync(companyModel.ImageId ?? 0);
            return companyModel;
        }

        public Task<string?> GetUrlAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, CompanyModel model)
        {
            throw new NotImplementedException();
        }
    }
}
