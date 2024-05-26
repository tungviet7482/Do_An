using Demo.Domain.Common;
using Demo.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Demo.Domain.Entity
{
    public class Transaction: BaseEntity
    {
        public DateTime TransactionDate { get; set; }
        public decimal Fee { get; set; }
        public string Message { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public bool Deleted { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Transaction_ServicePackage> Transaction_ServicePackages { get; set; }
    }
}
