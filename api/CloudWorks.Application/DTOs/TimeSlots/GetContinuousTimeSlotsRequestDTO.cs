namespace CloudWorks.Application.DTOs.TimeSlots
{
    public class GetContinuousTimeSlotsRequestDTO : TimeSlotDTO
    {
        public List<Guid> AccessPointIds { get; set; }
        public Guid UserId { get; set; }
    }
}