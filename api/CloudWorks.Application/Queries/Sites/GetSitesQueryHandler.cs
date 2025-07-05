using AutoMapper;
using CloudWorks.Application.Constants;
using CloudWorks.Application.DTOs.Pagination;
using CloudWorks.Application.DTOs.Sites;
using CloudWorks.Application.Services;
using CloudWorks.Services.Contracts.Sites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CloudWorks.Application.Queries.Sites
{
    public class GetSitesQueryHandler : IRequestHandler<GetSitesQuery, PagedResult<SiteDTO>>
    {
        private readonly ISiteRepository _siteRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public GetSitesQueryHandler(ISiteRepository siteRepository, IMapper mapper, ICurrentUserService currentUser)
        {
            _siteRepository = siteRepository;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<PagedResult<SiteDTO>> Handle(GetSitesQuery request, CancellationToken cancellationToken)
        {
            var filter = request.PagingFilter;

            var query = _siteRepository.Query();

            if (!string.IsNullOrWhiteSpace(filter.Search))
                query = query.Where(s => s.Name.Contains(filter.Search));

            if (!_currentUser.HasRole(UserRoles.Administrator))
            {
                var profileId = await _currentUser.GetProfileIdAsync();

                query = query.Where(site =>
                    site.Profiles.Any(p => p.ProfileId == profileId));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(s => s.Name)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync(cancellationToken);

            var result = new PagedResult<SiteDTO>
            {
                Items = _mapper.Map<List<SiteDTO>>(items),
                TotalCount = totalCount,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };

            return result;
        }
    }
}