namespace CloudWorks.Data.Contracts.Entities
{
    public class AccessEvent
    {
        public Guid Id { get; set; }

        public Guid SiteId { get; set; }
        public Site? Site { get; set; }

        public Guid AccessPointId { get; set; }
        public AccessPoint? AccessPoint { get; set; }

        public Guid ProfileId { get; set; }
        public Profile? Profile { get; set; }

        public bool Success { get; set; }

        public DateTime Timestamp { get; set; }

        public string Reason { get; set; } = string.Empty;
    }
}