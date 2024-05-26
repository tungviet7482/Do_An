using AutoMapper;
using Demo.Application.Interface;
using Demo.Application.Model;
using Demo.Application.Service.Base;
using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.Enum;
using Demo.Domain.filter;
using Demo.Domain.IRepository;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Demo.Application.Service
{
    public class JobService : BaseService, IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMediaService _mediaService;

        public JobService(IMapper mapper, IJobRepository jobRepository, ICompanyRepository companyRepository, UserManager<User> userManager, IMediaService mediaService) : base(mapper)
        {
            _jobRepository = jobRepository;
            _companyRepository = companyRepository;
            _userManager = userManager;
            _mediaService = mediaService;
        }

        public async Task<bool> CreateAsync(JobModel model)
        {
            var users = _userManager.Users;
            var user = users.Where(x => !x.Deleted && x.Id == model.UserId).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            if (model == null)
            {
                return false;
            }
            var job = _mapper.Map<Job>(model);
            job.StartDate = DateTime.Now;
            var slug = GenerateSlug(job.Title);
            job.Slug = slug;
            var i = 1;
            while (await _jobRepository.CheckDuplicateSlugJob(job.Slug))
            {
                job.Slug = $"{slug}-{i}";
                i++;
            }
            var isSuccess = await _jobRepository.CreateAsync(job);
            var jobUser = new Job_User
            {
                JobId = job.Id,
                UserId = model.UserId ?? 0,
            };
            return isSuccess && await _jobRepository.CreateJobUser(jobUser);
        }

        public async Task<List<JobModel>?> GetAllAsync()
        {
            var jobs = await _jobRepository.GetAllAsync();
            if (jobs == null)
            {
                return null;
            }
            var jobsModel = _mapper.Map<List<JobModel>>(jobs);

            return jobsModel;
        }

        public async Task<DataObject<JobModel>> GetPagingAsync(FilterJob filter)
        {
            var jobs = await _jobRepository.GetPagingAsync(filter);
            var jobsModel = _mapper.Map<List<JobModel>>(jobs.Items);
            foreach (var job in jobsModel)
            {
                job.CompanyPreviewImgUrl = await _mediaService.GetUrlAsync(job.CompanyPreviewImageId ?? 0);
                job.CompanyImgUrl = await _mediaService.GetUrlAsync(job.CompanyImageId ?? 0);
            }

            return new DataObject<JobModel>(jobsModel, jobs.TotalRecord);
        }
        public async Task<JobModel?> GetJobBySlug(string slug)
        {
            var filter = new FilterJob
            {
                Slug = slug,
            };
            var jobs = await _jobRepository.GetPagingAsync(filter);
            var jobModel = _mapper.Map<List<JobModel>>(jobs.Items)?.FirstOrDefault();
            if(jobModel == null)
            {
                return null;
            }       
            jobModel.CompanyPreviewImgUrl = await _mediaService.GetUrlAsync(jobModel.CompanyPreviewImageId ?? 0);
            jobModel.CompanyImgUrl = await _mediaService.GetUrlAsync(jobModel.CompanyImageId ?? 0);
            return jobModel;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            return await _jobRepository.DeleteAsync(job);
        }

        public Task<string?> GetUrlAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(int id, JobModel model)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            if (job == null || model == null)
            {
                return false;
            }

            job!.ShortDescription = model.ShortDescription;
            job!.Body = model.Body;
            job!.Published = model.Published;
            job!.Quantity = model.Quantity;
            job!.MinSalary = model.MinSalary;
            job!.MaxSalary = model.MaxSalary;
            job!.WorkExperience = model.WorkExperience;
            job!.Address = model.Address;
            job!.StartDate = model.StartDate;
            job!.EndDate = model.EndDate;
            job!.UpdateAt = DateTime.Now;
            job!.Deleted = model.Deleted;
            job!.JobType = model.JobType;
            job!.CategoryId = model.CategoryId;
            if(job.Title != model.Title)
            {
                job!.Title = model.Title;
                var slug = GenerateSlug(job.Title);
                job.Slug = slug;
                var i = 1;
                while (await _jobRepository.CheckDuplicateSlugJob(job.Slug))
                {
                    job.Slug = $"{slug}-{i}";
                    i++;
                }
            }

            var isSuccess = await _jobRepository.UpdateAsync(job);

            return isSuccess;
        }
    }
}
