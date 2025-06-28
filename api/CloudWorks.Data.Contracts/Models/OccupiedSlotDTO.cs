using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Data.Contracts.Models
{
    public class OccupiedSlotDto
    {
        public Guid AccessPointId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
