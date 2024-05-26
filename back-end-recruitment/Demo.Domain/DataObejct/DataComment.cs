using Demo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.DataObejct
{
    public class DataComment<TKey> : DataObject<TKey>
    {
        public int[] StarRating { get; set; } = new int[5];
        public float avgStars { get; set; }
        public DataComment(List<TKey> list, int t, int[] sr, float avgStars) : base(list, t)
        {
            StarRating = sr;
            this.avgStars = avgStars;
        }
    }
}
