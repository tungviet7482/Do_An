using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Model
{
    public class ContactUsModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool Processed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class  ContactUsModelValidator : AbstractValidator<ContactUsModel>
    {
        public ContactUsModelValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty();
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress().WithMessage("Định dạng email không hợp lệ")
                .Must(x => x != null && !x.Trim().Contains(" ")).WithMessage("Email không được chứa khoảng trắng");
            RuleFor(x => x.Phone)
                .NotEmpty()
                .Matches(@"^\s*(032|033|034|035|036|037|038|039|096|097|098|086|083|084|085|081|082|088|091|094|070|079|077|076|078|090|093|089|056|058|092|059|099)[0-9]{7,8}\s*$").WithMessage("Số điện thoại lỗi định dạng");
            RuleFor(x => x.Message).NotEmpty();
        }
    }
}
