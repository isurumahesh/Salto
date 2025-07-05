using CloudWorks.Application.DTOs.Schedules;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Validators
{
    public class ScheduleRequestValidator : AbstractValidator<ScheduleRequestDTO>
    {
        public ScheduleRequestValidator()
        {
            RuleFor(x => x.StartUtc)
             .GreaterThan(DateTime.UtcNow)
             .WithMessage("Start time must be in the future.");

            RuleFor(x => x.EndUtc)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("End time must be in the future.");

            RuleFor(x => x)
                .Must(x => x.StartUtc < x.EndUtc)
                .WithMessage("Start time must be before end time.");
        }
    }
}
