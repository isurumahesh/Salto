using CloudWorks.Application.DTOs.Sites;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Validators
{
    public class SiteValidator : AbstractValidator<AddSiteDTO>
    {
        public SiteValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(10);
        }
    }
}
