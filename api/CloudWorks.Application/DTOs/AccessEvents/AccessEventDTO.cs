using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.DTOs.AccessEvents
{
    public record AccessEventDTO
    {
        public Guid Id { get; init; }
        public Guid SiteId { get; init; }
        public Guid AccessPointId { get; init; }
        public string AccessPointName { get; init; } = string.Empty;
        public Guid ProfileId { get; init; }
        public string Email { get; init; } = string.Empty;
        public bool Success { get; init; }
        public DateTime Timestamp { get; init; }
        public string Reason { get; init; } = string.Empty;
    }
}
