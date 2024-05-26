using Demo.Application.Model.Base;
using Demo.Domain.Enum;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? FullName { set; get; }
        public DateTime Dob { set; get; }
        public string? Email { set; get; }
        public string? PhoneNumber { set; get; }
        public string? UserName { set; get; }
        public string? Password { set; get; }
        public bool RemeberMe { set; get; } 
        public List<string>? Roles { set; get; }
        public string? ConfirmPassword { set; get; }
        public int? CompanyId { get; set; }
        public long? FileCVId { get; set; }
        public string? FileCVUrl { get; set; }
        public long? AvatarId { get; set; }
        public string? AvatarUrl { get; set; }
        public int? LocationId { get; set; }
        public string? Technology { get; set; }
        public int? FieldId { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyDescription { get; set; }
        public string? CompanyEmployeeCount { get; set; }
        public string? CompanyWebsiteUrl { get; set; }
        public string? CompanyAddress { get; set; }
        public int? CompanyImageId { get; set; }
        public int? CompanyPreviewImageId { get; set; }
        public string? PreviewImgUrl { get; set; }
        public string? ImgUrl { get; set; }
    }
    
    public class UserModelValidator: AbstractValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Bắt buộc")
                .MaximumLength(200).WithMessage("Tối đa 200 ký tự");
            RuleFor(x => x.Dob).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Ngày sinh không hợp lệ");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Bắt buộc")
                .EmailAddress().WithMessage("Email không hợp lệ");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Bắt buộc")
                .Matches(@"^\s*(032|033|034|035|036|037|038|039|096|097|098|086|083|084|085|081|082|088|091|094|070|079|077|076|078|090|093|089|056|058|092|059|099)[0-9]{7,8}\s*$")
                .WithMessage("Số điện thoại lỗi định dạng");
            RuleFor(x => x)
                .Must(x => x.ConfirmPassword == x.Password).WithMessage("Mật khẩu không khớp");

        }
    }

    public class UserModelLoginValidator : AbstractValidator<UserModel>
    {
        public UserModelLoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Bắt buộc")
                .EmailAddress().WithMessage("email không hợp lê");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Tối thiểu 6 kí tự").Length(5, 30).WithMessage("Từ 6 đến 30");
        }
    }
}
