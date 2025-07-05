namespace CloudWorks.Application.DTOs.Sites
{
    public record SiteDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
    }
}