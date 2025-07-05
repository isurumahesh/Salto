namespace CloudWorks.Application.DTOs.TimeSlots
{
    public record GetFreeTimeSlotsRequestDTO : TimeSlotDTO
    {
        public List<Guid> AccessPointIds { get; init; } = [];
    }
}