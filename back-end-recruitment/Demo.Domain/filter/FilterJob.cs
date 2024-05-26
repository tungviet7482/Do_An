using Demo.Domain.Common;
using Demo.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.filter
{
    public class FilterJob : BaseFilter
    {
        public JobType? JobType { set; get; }
        public int? JobId { set; get; }
        public int? Salary { set; get; }
        public List<int>? JobIds { set; get; }
        public int? LocationId { get; set; }
        public int? CategoryId { get; set; }
        public int? WorkExperience { get; set; }
        public bool FollowedJob { set; get; }
        public JobStatus? JobStatus { get; set; }
    }
}
