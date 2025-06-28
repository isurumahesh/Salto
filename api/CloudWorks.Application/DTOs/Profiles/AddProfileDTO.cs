using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.DTOs.Profiles
{
    public class AddProfileDTO
    {
        public string Email { get; set; } = default!;
        public Guid? IdentityId { get; set; }
    }
}
