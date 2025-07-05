namespace CloudWorks.Application.DTOs.SiteProfiles
{
    public record AddSiteProfileDTO
    {
        public Guid ProfileId { get; init; }
        public Guid SiteId { get; init; }
    }
}