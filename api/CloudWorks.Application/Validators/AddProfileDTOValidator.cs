using CloudWorks.Application.DTOs.Profiles;
using FluentValidation;

namespace CloudWorks.Application.Validators
{
    public class AddProfileDTOValidator : AbstractValidator<AddProfileDTO>
    {
        public AddProfileDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.");

            RuleFor(x => x.IdentityId)
                .Must(id => id != Guid.Empty)
                .WithMessage("IdentityId cannot be an empty GUID.");
        }
    }
}