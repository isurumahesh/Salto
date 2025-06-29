namespace CloudWorks.Application.DTOs.TimeSlots
{
    public class GetFreeTimeSlotsRequestDTO : TimeSlotDTO
    {
        public List<Guid> AccessPointIds { get; set; }
    }
}