using Demo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Entity
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Deleted { get; set; }
        public bool IsPublished { get; set; }
        public int DisplayOrder { get; set; }
        public string ShortDescription { get; set; }
        public int ImageId { get; set; }
        public int PreviewImageId { get; set; }
        public string Slug { get; set; }
        public DateTime CreatAt {  get; set; }

    }
}
