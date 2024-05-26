using Demo.Domain.Common;

namespace Demo.Domain.Entity
{
    public class MediaFile
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int FolderId { get; set; }

        public virtual MediaFolder MediaFolder { get; set; }
    }
}
