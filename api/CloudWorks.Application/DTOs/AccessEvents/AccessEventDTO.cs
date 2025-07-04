using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.DTOs.AccessEvents
{
    public class AccessEventDTO
    {
        public Guid Id { get; set; }
        public Guid SiteId { get; set; }
        public Guid AccessPointId { get; set; }
        public string AccessPointName { get; set; } = string.Empty;
        public Guid ProfileId { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool Success { get; set; }
        public DateTime Timestamp { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
