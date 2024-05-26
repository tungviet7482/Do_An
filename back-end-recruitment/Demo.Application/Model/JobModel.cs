using Demo.Domain.Enum;
using FluentValidation;

namespace Demo.Application.Model
{
    public class JobModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ShortDescription { get; set; }
        public int Quantity { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public int WorkExperience { get; set; }
        public JobType JobType { get; set; }
        public string Address { get; set; }
        public bool Published { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool Deleted { get; set; }
        public string? Slug { get; set; }
        public long? ImageId { get; set; }
        public long? PreviewImageId { get; set; }
        public int? CategoryId { get; set; }
        public int? LocationId { get; set; }
        public string? LocationName { get; set; }
        public int? UserId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { set; get; }
        public DateTime? Dob { get; set; }
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyDescription { get; set; }
        public string? CompanyEmployeeCount { get; set; }
        public string? CompanyWebsiteUrl { get; set; }
        public string? CompanyAddress { get; set; }
        public long? CompanyImageId { get; set; }
        public long? CompanyPreviewImageId { get; set; }
        public string? CompanyPreviewImgUrl { get; set; }
        public string? CompanyImgUrl { get; set; }
        public string? CompanySlug { get; set; }
    }

    public class JobModelValidator : AbstractValidator<JobModel>
    {
        public JobModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bắt buộc");
            RuleFor(x => x.Body).NotEmpty().WithMessage("Bắt buộc");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Bắt buộc");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Bắt buộc");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("Bắt buộc");

        }
    }


}
