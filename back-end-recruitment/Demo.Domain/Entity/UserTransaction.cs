using Demo.Domain.Enum;

namespace Demo.Domain.Entity
{
    public class UserTransaction
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Fee { get; set; }
        public string Message { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? UserId { get; set; }
        public bool Deleted { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string TypeUser { get; set; }
    }
}
