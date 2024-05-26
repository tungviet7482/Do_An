using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Demo.Domain.Entity
{
    public class Comment
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Advantages { get; set; }
        public string Disadvantages { get; set; }
        public int UserId {  get; set; }
        public int CompanyId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt {  get; set; }
        public bool IsPublish {  get; set; }
        public int Rate { get; set; }
    }
}
