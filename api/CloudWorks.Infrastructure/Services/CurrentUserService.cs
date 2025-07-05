using CloudWorks.Application.Services;
using CloudWorks.Data.Database;
using Duende.IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CloudWorks.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CloudWorksDbContext _context;
        private Guid? _cachedProfileId;

        public CurrentUserService(IHttpContextAccessor accessor, CloudWorksDbContext context)
        {
            _httpContextAccessor = accessor;
            _context = context;
        }

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        public string? Email => User?.FindFirstValue(JwtClaimTypes.Email);

        public bool HasRole(string role) => User?.IsInRole(role) ?? false;

        public bool HasScope(string scope) =>
            User?.FindAll("scope").Any(c => c.Value == scope) ?? false;

        public async Task<Guid?> GetProfileIdAsync()
        {
            if (_cachedProfileId != null)
                return _cachedProfileId;

            var identityIdClaim = User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (Guid.TryParse(identityIdClaim, out var identityId))
            {
                var profile = await _context.Profiles
                    .Where(p => p.IdentityId == identityId)
                    .Select(p => new { p.Id })
                    .FirstOrDefaultAsync();

                _cachedProfileId = profile?.Id;
            }

            return _cachedProfileId;
        }
    }
}