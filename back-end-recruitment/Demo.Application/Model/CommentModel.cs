using FluentValidation;

namespace Demo.Application.Model
{
    public class CommentModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Advantages { get; set; }
        public string Disadvantages { get; set; }
        public int? UserId { get; set; }
        public int CompanyId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsPublish { get; set; }
        public int Rate { get; set; }
    }

    public class CommentModelValidator : AbstractValidator<CommentModel>
    {
        public CommentModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bắt buộc");
            RuleFor(x => x.Advantages).NotEmpty().WithMessage("Bắt buộc");
            RuleFor(x => x.Disadvantages).NotEmpty().WithMessage("Bắt buộc");
            RuleFor(x => x.CompanyId).NotEmpty().WithMessage("Bắt buộc");
            RuleFor(x => x.Rate).NotEmpty().WithMessage("Bắt buộc").InclusiveBetween(1, 5).WithMessage("Số sao không hợp lệ"); ;
        }
    }
}
