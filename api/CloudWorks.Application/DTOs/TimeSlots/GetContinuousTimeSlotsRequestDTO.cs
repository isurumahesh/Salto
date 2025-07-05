namespace CloudWorks.Application.DTOs.TimeSlots
{
    public record GetContinuousTimeSlotsRequestDTO : TimeSlotDTO
    {
        public List<Guid> AccessPointIds { get; set; }
        public Guid ProfileId { get; set; }
    }
}