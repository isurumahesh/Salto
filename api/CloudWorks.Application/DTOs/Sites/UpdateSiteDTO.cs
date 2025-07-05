namespace CloudWorks.Application.DTOs.Sites
{
    public record UpdateSiteDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}