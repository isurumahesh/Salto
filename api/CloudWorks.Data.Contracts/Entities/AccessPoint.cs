namespace CloudWorks.Data.Contracts.Entities;

public sealed class AccessPoint
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid SiteId { get; set; }

    public Site? Site { get; set; }
}