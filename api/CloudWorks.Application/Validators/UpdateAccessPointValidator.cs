using CloudWorks.Application.DTOs.AccessPoints;
using FluentValidation;

namespace CloudWorks.Application.Validators
{
    public class UpdateAccessPointValidator : AbstractValidator<UpdateAccessPointDTO>
    {
        public UpdateAccessPointValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }
}