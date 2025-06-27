using CloudWorks.Application.DTOs;
using CloudWorks.Application.DTOs.Schedules;
using CloudWorks.Services.Contracts.Schedules;
using MediatR;

namespace CloudWorks.Application.Queries.Schedules
{
    public class GetUserContinuousAccessHandler : IRequestHandler<GetUserContinuousAccessQuery, List<AccessPointTimeSlotDto>>
    {
        private readonly IScheduleRepository _repository;

        public GetUserContinuousAccessHandler(IScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AccessPointTimeSlotDto>> Handle(GetUserContinuousAccessQuery request, CancellationToken cancellationToken)
        {
            var accessRanges = await _repository.GetUserAccessRangesAsync(request.UserId, request.AccessPointIds, request.Start, request.End, cancellationToken);

            return accessRanges
                .GroupBy(x => x.AccessPointId)
                .Select(group => new AccessPointTimeSlotDto
                {
                    AccessPointId = group.Key,
                    TimeSlots = group.Select(g => new TimeSlotDTO { Start = g.Start, End = g.End }).ToList()
                })
                .ToList();
        }
    }
}