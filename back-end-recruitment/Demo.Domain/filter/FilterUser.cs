using Demo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.filter
{
    public class FilterUser: BaseFilter
    {
        public int? UserId { get; set; }
    }
}
