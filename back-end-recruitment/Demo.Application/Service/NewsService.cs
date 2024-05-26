using AutoMapper;
using Demo.Application.Interface;
using Demo.Application.Model;
using Demo.Application.Service.Base;
using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.Enum;
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
    public class NewsService : BaseService ,INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMediaService _mediaService;
        public NewsService(IMapper mapper, INewsRepository newsRepository, IMediaService mediaService) : base(mapper)
        {
            _newsRepository = newsRepository;
            _mediaService = mediaService;
        }

        public async Task<bool> CreateAsync(NewsModel model)
        {
            var news = _mapper.Map<News>(model);
            news!.CreatAt = DateTime.Now;
            var slug = GenerateSlug(news.Title);
            news.Slug = slug;
            var i = 1;
            while (await _newsRepository.CheckDuplicateSlugNews(news.Slug))
            {
                news.Slug = $"{slug}-{i}";
                i++;
            }
            var isSuccess = await _newsRepository.CreateAsync(news);
            if (isSuccess)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var news = await _newsRepository.GetByIdAsync(id);
            if (news == null)
            {
                return false;
            }
            var isSuccessed = await _newsRepository.DeleteAsync(news);
            return isSuccessed;
        }

        public Task<List<NewsModel>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<DataObject<NewsModel>> GetPagingAsync(FilterNews filter)
        {
            var news = await _newsRepository.GetPagingAsync(filter);
            var newsModels = _mapper.Map<List<NewsModel>>(news.Items);
            foreach (var item in newsModels)
            {
                item.ImgUrl = await _mediaService.GetUrlAsync(item.ImageId ?? 0);
                item.PreviewImgUrl = await _mediaService.GetUrlAsync(item.PreviewImageId ?? 0);
            }

            return new DataObject<NewsModel>(newsModels, news.TotalRecord);
        }

        public async Task<bool> UpdateAsync(int id, NewsModel model)
        {
            var news = await _newsRepository.GetByIdAsync(id);
            if (news == null)
            {
                return false;
            }

            news.Title = model.Title;
            news.Body = model.Body;
            news.DisplayOrder = model.DisplayOrder ?? 0;
            news.IsPublished = model.IsPublished;
            news.ShortDescription = model.ShortDescription;
            news.PreviewImageId = model.PreviewImageId ?? 0;
            news.ImageId = model.ImageId ?? 0;
            var slug = GenerateSlug(news.Title);
            news.Slug = slug;
            var i = 1;
            while (await _newsRepository.CheckDuplicateSlugNews(news.Slug))
            {
                news.Slug = $"{slug}-{i}";
                i++;
            }
            var isSuccessed = await _newsRepository.UpdateAsync(news);

            return isSuccessed;
        }

        public async Task<NewsModel?> GetNewsBySlug(string slug)
        {
            var filter = new FilterNews
            {
                Slug = slug,
                Published = true
            };
            var news = await _newsRepository.GetPagingAsync(filter);
            var newsModel = _mapper.Map<List<NewsModel>>(news.Items)!.FirstOrDefault();
            if (newsModel == null)
            {
                return null;
            }
            newsModel!.PreviewImgUrl = await _mediaService.GetUrlAsync(newsModel.PreviewImageId ?? 0);
            newsModel.ImgUrl = await _mediaService.GetUrlAsync(newsModel.ImageId ?? 0);
            return newsModel;
        }
    }
}
