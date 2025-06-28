using CloudWorks.Application.DTOs.AccessPoints;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
