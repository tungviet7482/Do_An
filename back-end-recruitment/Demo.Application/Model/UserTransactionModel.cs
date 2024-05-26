using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Demo.Application.Model
{
    public class UserTransactionModel
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
