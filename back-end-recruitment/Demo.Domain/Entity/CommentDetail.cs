using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Entity
{
    public class CommentDetail
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Advantages { get; set; }
        public string Disadvantages { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Rate { get; set; }
        public bool IsPublish { get; set; }
        public int[] starRatings { get; set; } = new int[5];
    }
}
