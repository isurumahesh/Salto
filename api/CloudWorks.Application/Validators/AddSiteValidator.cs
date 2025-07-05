using CloudWorks.Application.DTOs.Sites;
using FluentValidation;

namespace CloudWorks.Application.Validators
{
    public class AddSiteValidator : AbstractValidator<AddSiteDTO>
    {
        public AddSiteValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }
}