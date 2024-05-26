using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Common
{
    public class DataObject<TKey>
    {
        public List<TKey> Items { set; get; }
        public int TotalRecord { set; get; }
        public int? pageSize { get; set; }
        public int? currentPage { get; set; }
        public DataObject(List<TKey> list, int t)
        {
            Items = list;
            TotalRecord = t;
        }

        public DataObject(List<TKey> items, int toTalRecord, int? pageSize, int? currentPage) : this(items, toTalRecord)
        {
            this.pageSize = pageSize;
            this.currentPage = currentPage;
        }
    }
}
