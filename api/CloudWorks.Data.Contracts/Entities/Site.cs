using CloudWorks.Data.Contracts.Entities;

public sealed class Site
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<SiteProfile> Profiles { get; set; } = [];
}