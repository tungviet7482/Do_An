using Demo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Entity
{
    public class Company: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmployeeCount { get; set; }
        public string WebsiteUrl { get; set; }
        public string Address { get; set; }
        public long? ImageId { get; set; }
        public long? PreviewImageId { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string Slug { get; set; }
        public virtual User User { get; set; }
    }
}
