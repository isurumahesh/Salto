using CloudWorks.Application.DTOs.Schedules;
using MediatR;

namespace CloudWorks.Application.Queries.Schedules
{
    public record GetUserContinuousAccessQuery(Guid UserId, List<Guid> AccessPointIds, DateTime Start, DateTime End, CancellationToken CancellationToken) : IRequest<List<AccessPointTimeSlotDto>>;
}