using Demo.Application.Model;
using Demo.Application.Model.Base;
using Demo.Domain.Common;
using FluentValidation;

namespace Demo.Domain.Entity
{
    public class ServicePackageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameSystem { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public int Quota { get; set; }
        public bool Deleted { get; set; }
    }

    public class ServicePackageModelValidator : AbstractValidator<ServicePackageModel>
    {
        public ServicePackageModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bắt buộc");
            RuleFor(x => x.Quota).NotEmpty().WithMessage("Bắt buộc").GreaterThan(0);
        }
    }
}
