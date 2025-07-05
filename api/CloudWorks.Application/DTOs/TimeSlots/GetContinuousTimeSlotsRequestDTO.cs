using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.DTOs.TimeSlots
{
    public record GetContinuousTimeSlotsRequestDTO:TimeSlotDTO
    {
        public List<Guid> AccessPointIds { get; set; }
        public Guid ProfileId { get; set; }
    }
}
