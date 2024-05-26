using FluentValidation;

namespace Demo.Application.Model
{
    public class NewsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Deleted { get; set; }
        public bool IsPublished { get; set; }
        public int? DisplayOrder { get; set; }
        public string ShortDescription { get; set; }
        public int? ImageId { get; set; }
        public int? PreviewImageId { get; set; }
        public DateTime CreatAt { get; set; }
        public string? PreviewImgUrl { get; set; }
        public string? ImgUrl { get; set; }
        public string? Slug { get; set; }
    }

    public class NewsModelValidator : AbstractValidator<NewsModel>
    {
        public NewsModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bắt buộc");
            RuleFor(x => x.Body).NotEmpty().WithMessage("Bắt buộc");
            RuleFor(x => x.ShortDescription).NotEmpty().WithMessage("Bắt buộc");
        }
    }

}
