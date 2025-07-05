using CloudWorks.Application.DTOs.Bookings;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Validators
{
    public class AddBookingRequestValidator : AbstractValidator<AddBookingDTO>
    {
        public AddBookingRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.SiteProfiles)
                .NotNull().WithMessage("SiteProfiles list is required.")
                .Must(list => list.Any()).WithMessage("At least one SiteProfile is required.");

            RuleFor(x => x.AccessPoints)
                .NotNull().WithMessage("AccessPoints list is required.")
                .Must(list => list.Any()).WithMessage("At least one AccessPoint is required.");

            RuleFor(x => x.Schedules)
                .NotNull().WithMessage("Schedules list is required.")
                .Must(list => list.Any()).WithMessage("At least one schedule is required.");

            RuleForEach(x => x.Schedules)
                .SetValidator(new ScheduleRequestValidator());
        }
    }
}
