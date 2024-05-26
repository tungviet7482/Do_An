using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Common
{
    public class Result<TKey>
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public BaseException? Error { get; set; }
        public TKey? Data { get; set; }
    }
}
