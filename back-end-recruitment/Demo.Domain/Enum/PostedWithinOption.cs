using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Enum
{
    public enum PostedWithinOption
    {
        None, 
        Last24Hours, 
        Last7Days, 
        Last30Days,
        ThisMonth, 
        LastMonth,
    }
}
