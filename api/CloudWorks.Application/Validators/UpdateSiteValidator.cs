using CloudWorks.Application.DTOs.Sites;
using FluentValidation;

namespace CloudWorks.Application.Validators
{
    public class UpdateSiteValidator : AbstractValidator<UpdateSiteDTO>
    {
        public UpdateSiteValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }
}