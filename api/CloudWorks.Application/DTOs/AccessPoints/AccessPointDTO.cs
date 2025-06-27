namespace CloudWorks.Application.DTOs.AccessPoints
{
    public class AccessPointDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public Guid SiteId { get; set; }
    }
}