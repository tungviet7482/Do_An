using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Model.Base
{
    public class PagingBase
    {
        public int PageIndex { set; get; } = 0;
        public int PageSize { set; get; } = 20;
    }
}
