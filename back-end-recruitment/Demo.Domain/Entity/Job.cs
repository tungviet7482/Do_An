using Demo.Domain.Common;
using Demo.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Entity
{
    public class Job: BaseEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string ShortDescription { get; set; }
        public int Quantity { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public int WorkExperience { get; set; }
        public JobType JobType { get; set; }
        public string Address { get; set; }
        public bool Published { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string Slug { get; set; }
        public bool Deleted { get; set; }
        public long? ImageId { get; set; }
        public long? PreviewImageId { get; set; }
        public int? CategoryId { get; set; }
        public int? LocationId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Location? Location { get; set; }
        public virtual ICollection<Job_User>? Job_Users{ get; set; }

    }
}
