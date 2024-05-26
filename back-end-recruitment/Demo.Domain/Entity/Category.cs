using Demo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Entity
{
   
    public class Category: BaseEntity
    {
        public string? Name { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int DisplayOrder { get; set; }
        public virtual ICollection<Job>? Jobs { get; set; }
    }
}
