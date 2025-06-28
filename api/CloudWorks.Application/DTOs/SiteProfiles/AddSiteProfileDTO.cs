using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.DTOs.SiteProfiles
{
    public class AddSiteProfileDTO
    {
        public Guid ProfileId { get; set; }
        public Guid SiteId { get; set; }
    }
}
