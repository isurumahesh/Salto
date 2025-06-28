using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.DTOs.TimeSlots
{
    public class GetFreeTimeSlotsRequestDTO:TimeSlotDTO
    {
        public List<Guid> AccessPointIds { get; set; }     
    }
}
