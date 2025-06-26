using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.DTOs.AccessPoints
{
    public class UpdateAccessPointDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid SiteId { get; set; }
    }
}
