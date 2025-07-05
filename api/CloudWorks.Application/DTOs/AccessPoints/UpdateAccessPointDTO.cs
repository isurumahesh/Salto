namespace CloudWorks.Application.DTOs.AccessPoints
{
    public record UpdateAccessPointDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public Guid SiteId { get; init; }
    }
}