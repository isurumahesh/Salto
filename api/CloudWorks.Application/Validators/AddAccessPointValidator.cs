using CloudWorks.Application.DTOs.AccessPoints;
using FluentValidation;

namespace CloudWorks.Application.Validators
{
    public class AddAccessPointValidator : AbstractValidator<AddAccessPointDTO>
    {
        public AddAccessPointValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }
}