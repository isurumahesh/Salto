namespace CloudWorks.Application.DTOs.TimeSlots
{
    public record TimeSlotDTO
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}