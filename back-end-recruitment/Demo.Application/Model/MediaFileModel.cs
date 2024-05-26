using Demo.Domain.Common;
using Microsoft.AspNetCore.Http;

namespace Demo.Domain.Entity
{
    public class MediaFileModel
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string Url { get; set; }
        public long Size { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int FolderId { get; set; }
        public IFormFile File { get; set; }
    }
}
