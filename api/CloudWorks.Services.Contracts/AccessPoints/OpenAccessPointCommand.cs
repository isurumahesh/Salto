namespace CloudWorks.Services.Contracts.AccessPoints;

public class OpenAccessPointCommand
{
    public Guid ProfileId { get; set; }
    public Guid AccessPointId { get; set; }
}