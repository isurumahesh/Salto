namespace CloudWorks.Data.Contracts.Entities;

public sealed class Booking
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public Guid SiteId { get; set; }
    public Site? Site { get; set; }

    public ICollection<SiteProfile> Profiles { get; set; } = [];
    public ICollection<Schedule> Schedules { get; set; } = [];
    public ICollection<AccessPoint> AccessPoints { get; set; } = [];
}