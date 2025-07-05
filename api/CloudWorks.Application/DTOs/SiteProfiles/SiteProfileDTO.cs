namespace CloudWorks.Application.DTOs.SiteProfiles
{
    public record SiteProfileDTO
    {
        public Guid Id { get; init; }

        public Guid ProfileId { get; init; }

        public Guid SiteId { get; init; }
    }
}