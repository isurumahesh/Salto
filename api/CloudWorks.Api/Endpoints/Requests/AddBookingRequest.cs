namespace CloudWorks.Api.Endpoints.Requests;

public class AddBookingRequest
{
    public string Name { get; set; }
    public List<string> Users { get; set; } = [];
    public List<Guid> AccessPoints { get; set; } = [];
    public List<ScheduleRequest> Schedules { get; set; } = [];
}