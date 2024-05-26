using AutoMapper;
using Demo.Application.Interface;
using Demo.Application.Model;
using Demo.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Mapper
{
    public class MappingProfile : Profile
    {

        //public MappingProfile(IMediaService mediaService)
        //{
        //    _mediaService = mediaService;
        //}
        public MappingProfile()
        {
            CreateMap<MediaFileModel, MediaFile>()
                .ForMember(x => x.CreateAt, y => y.MapFrom(src => DateTime.Now))
                .ForMember(x => x.UpdateAt, y => y.MapFrom(src => DateTime.Now))
                .ForMember(x => x.Size, y => y.MapFrom(src => src.File.Length))
                .ForMember(x => x.FileName, y => y.MapFrom(src => src.File.FileName))
                .ForMember(x => x.Extension, y => y.MapFrom(src => Path.GetExtension(src.File.FileName)))
                .ForMember(x => x.FileName, y => y.MapFrom(src => src.File.FileName));

            CreateMap<JobModel, Job>()
                .ForMember(x => x.CreateAt, y => y.MapFrom(src => DateTime.Now))
                .ForMember(x => x.UpdateAt, y => y.MapFrom(src => DateTime.Now))
                .ReverseMap();

            CreateMap<User, UserModel>()
                .ReverseMap();


            CreateMap<ServicePackage, ServicePackageModel>()
                .ForMember(x => x.Duration, y => y.MapFrom(src => src.Duration / (30 * 24 * 60 * 60)))
                .ReverseMap()
                .ForMember(x => x.Duration, y => y.MapFrom(src => src.Duration * (30 * 24 * 60 * 60)));

            CreateMap<TransactionModel, Transaction>()
               .ForMember(x => x.TransactionDate, y => y.MapFrom(src => DateTime.Now))
               .ForMember(x => x.StartDate, y => y.MapFrom(src => DateTime.Now))
               .ReverseMap();

            CreateMap<CategoryModel, Category>()
               .ForMember(x => x.UpdateAt, y => y.MapFrom(src => DateTime.Now))
               .ReverseMap();

            CreateMap<LocationModel, Location>()
              .ForMember(x => x.UpdateAt, y => y.MapFrom(src => DateTime.Now))
              .ReverseMap();

            CreateMap<UserTransactionModel, UserTransaction>()
              .ReverseMap();

            CreateMap<Job_UserModel, Job_User>()
              .ReverseMap();

            CreateMap<MediaFileModel, MediaFile>()
              .ReverseMap();

            CreateMap<Job_User_Company, JobModel>()
              .ReverseMap();

            CreateMap<Company, CompanyModel>()
              .ForMember(x => x.Email, y => y.MapFrom(src => src.User.Email))
              .ForMember(x => x.PhoneNumber, y => y.MapFrom(src => src.User.PhoneNumber))
              .ReverseMap();

            CreateMap<CategoryModel, Category_Job>()
              .ReverseMap();

            CreateMap<NewsModel, News>()
              .ReverseMap();

            CreateMap<Comment,CommentModel>()
                .ReverseMap();

            CreateMap<ContactUs, ContactUsModel>()
                .ReverseMap();
        }
    }
}
