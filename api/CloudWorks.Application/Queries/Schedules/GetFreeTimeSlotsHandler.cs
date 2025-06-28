using CloudWorks.Application.DTOs.Schedules;
using CloudWorks.Application.DTOs.TimeSlots;
using CloudWorks.Services.Contracts.Schedules;
using MediatR;

namespace CloudWorks.Application.Queries.Schedules
{
    public class GetFreeTimeSlotsHandler : IRequestHandler<GetFreeTimeSlotsQuery, List<AccessPointTimeSlotDto>>
    {
        private readonly IScheduleRepository _repository;

        public GetFreeTimeSlotsHandler(IScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AccessPointTimeSlotDto>> Handle(GetFreeTimeSlotsQuery request, CancellationToken cancellationToken)
        {
            var occupiedSlots = await _repository.GetOccupiedSlotsByAccessPointAsync(request.AccessPointIds, request.Start, request.End, cancellationToken);

            var grouped = occupiedSlots.GroupBy(x => x.AccessPointId);
            var results = new List<AccessPointTimeSlotDto>();

            foreach (var group in grouped)
            {
                var occupied = group.OrderBy(x => x.Start).ToList();
                var freeSlots = new List<TimeSlotDTO>();
                var current = request.Start;

                foreach (var slot in occupied)
                {
                    if (slot.Start > current)
                    {
                        freeSlots.Add(new TimeSlotDTO
                        {
                            Start = current,
                            End = slot.Start
                        });
                    }

                    if (slot.End > current)
                    {
                        current = slot.End;
                    }
                }

                if (current < request.End)
                {
                    freeSlots.Add(new TimeSlotDTO
                    {
                        Start = current,
                        End = request.End
                    });
                }

                results.Add(new AccessPointTimeSlotDto
                {
                    AccessPointId = group.Key,
                    TimeSlots = freeSlots
                });
            }

            return results;
        }
    }
}