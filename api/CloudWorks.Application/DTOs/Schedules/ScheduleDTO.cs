using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.DTOs.Schedules
{
    public record ScheduleDTO
    {
        public Guid Id { get; init; }

        public DateTime StartUtc { get; init; }
        public DateTime EndUtc { get; init; }

        public string Value { get; set; } 
    }
}
