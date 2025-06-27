namespace CloudWorks.Data.Contracts.Entities;

public sealed class SiteProfile
{
    public Guid Id { get; set; }

    public Guid ProfileId { get; set; }
    public Profile? Profile { get; set; }

    public Guid SiteId { get; set; }
    public Site? Site { get; set; }
}