using Demo.Application.Model;
using Demo.Application.Model.Base;
using Demo.Domain.Common;
using FluentValidation;
using System.Transactions;

namespace Demo.Domain.Entity
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Fee { get; set; }
        public string Message { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long? UserId { get; set; }
        public bool Deleted { get; set; }
    }

    public class TransactionModelValidator : AbstractValidator<TransactionModel>
    {
        public TransactionModelValidator()
        {
            RuleFor(x => x.Fee).NotEmpty().WithMessage("Bắt buộc");
        }
    }

}
