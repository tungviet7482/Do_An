using Microsoft.AspNetCore.Identity;

namespace Demo.Domain.Entity
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public int? CompanyId { get; set; }
        public bool Deleted { get; set; }
        public long? FileCVId { get; set; }
        public long? AvatarId { get; set; }
        public int LocationId { get; set; }
        public string? Technology { get; set; }
        public int FieldId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Job_User> Job_Users { get; set; }  
    }
}
