namespace CloudWorks.Application.DTOs.AccessPoints
{
    public record AccessPointDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public Guid SiteId { get; init; }
    }
}