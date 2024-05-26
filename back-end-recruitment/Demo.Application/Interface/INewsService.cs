using Demo.Application.Model;
using Demo.Domain.Common;
using Demo.Domain.filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Interface
{
    public interface INewsService
    {
        Task<bool> CreateAsync(NewsModel model);
        Task<bool> Delete(int id);
        Task<List<NewsModel>?> GetAllAsync();
        Task<DataObject<NewsModel>> GetPagingAsync(FilterNews page);
        Task<NewsModel?> GetNewsBySlug(string slug);
        Task<bool> UpdateAsync(int id, NewsModel model);
    }
}
