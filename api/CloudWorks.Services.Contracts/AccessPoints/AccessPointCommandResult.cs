namespace CloudWorks.Services.Contracts.AccessPoints;

public class AccessPointCommandResult<T> where T : class
{
    public T Command { get; set; } = null!;
    public DateTime TimeStamp { get; set; }
    public bool Success { get; set; }
    public string? Error { get; set; }
}