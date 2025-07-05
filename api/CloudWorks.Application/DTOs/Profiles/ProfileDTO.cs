using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.DTOs.Profiles
{
    public record ProfileDTO
    {
        public string Email { get; init; } 
        public Guid IdentityId { get; init; }
    }
}
