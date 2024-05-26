using Demo.Application.Model;
using Demo.Domain.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Interface
{
    public interface IMediaService
    {
        Task<bool> CreateAsync(MediaFileModel model);
        Task<MediaFileModel> DownloadFile(IFormFile fromFile);
        Task<List<MediaFileModel>> GetAllAsync();
        Task<string?> GetUrlAsync(long id);
        Task<bool> UpdateAsync(MediaFileModel model);
    }
}
