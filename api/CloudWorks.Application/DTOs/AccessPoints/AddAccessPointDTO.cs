namespace CloudWorks.Application.DTOs.AccessPoints
{
    public record AddAccessPointDTO
    {
        public string Name { get; init; }
        public Guid SiteId { get; init; }
    }
}