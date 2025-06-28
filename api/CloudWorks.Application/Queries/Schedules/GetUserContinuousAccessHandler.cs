using CloudWorks.Application.DTOs.Schedules;
using CloudWorks.Application.DTOs.TimeSlots;
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
                .Select(group =>
                {
                    var merged = MergeTimeSlots(
                        group.Select(g => new TimeSlotDTO { Start = g.Start, End = g.End })
                             .OrderBy(ts => ts.Start)
                             .ToList()
                    );
                    return new AccessPointTimeSlotDto
                    {
                        AccessPointId = group.Key,
                        TimeSlots = merged
                    };
                })
                .ToList();
        }

        private List<TimeSlotDTO> MergeTimeSlots(List<TimeSlotDTO> slots)
        {
            var merged = new List<TimeSlotDTO>();

            foreach (var slot in slots)
            {
                if (!merged.Any())
                {
                    merged.Add(new TimeSlotDTO { Start = slot.Start, End = slot.End });
                }
                else
                {
                    var last = merged.Last();
                    if (slot.Start <= last.End) // Overlap or adjacent
                    {
                        last.End = slot.End > last.End ? slot.End : last.End;
                    }
                    else
                    {
                        merged.Add(new TimeSlotDTO { Start = slot.Start, End = slot.End });
                    }
                }
            }
            return merged;
        }
    }
}