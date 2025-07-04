using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Services
{
    public interface ICurrentUserService
    {
        Task<Guid?> GetProfileIdAsync();
        string? Email { get; }
        bool HasRole(string role);
        bool HasScope(string scope);
    }
}
