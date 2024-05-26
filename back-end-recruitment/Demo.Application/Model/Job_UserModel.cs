using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Entity
{
    public class Job_UserModel
    {
        public int JobId { get; set; }
        public int UserId { get; set; }
        public bool Viewed { get; set; }
        public bool? Interested { get; set; }
        public bool FollowedJob { get; set; }
        public bool Applied { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
