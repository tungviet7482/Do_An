using Demo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.filter
{
    public class FilterCandidate: BaseFilter
    {
        public int? JobId { get; set; }
        public bool Viewed { get; set; }
        public bool? Applied { get; set; }
        public bool? Interested { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public int? LocationId { get; set; }
        public string? Technology { get; set; }
        public int? FieldId {  get; set; }
    }
}
