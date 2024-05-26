using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.filter;
using Demo.Domain.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.IRepository
{
    public interface INewsRepository : IBaseRepository<News, int>
    {
        Task<bool> CheckDuplicateSlugNews(string slug);
        Task<DataObject<News>> GetPagingAsync(FilterNews filter);
    }
}
