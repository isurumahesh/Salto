using AutoMapper;
using CloudWorks.Application.Constants;
using CloudWorks.Application.DTOs.Pagination;
using CloudWorks.Application.DTOs.Sites;
using CloudWorks.Data.Contracts.Models;
using CloudWorks.Services.Contracts.Sites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CloudWorks.Application.Queries.Sites
{
    public class GetSitesQueryHandler : IRequestHandler<GetSitesQuery, PagedResult<SiteDTO>>
    {
        private readonly ISiteRepository _siteRepository;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public GetSitesQueryHandler(ISiteRepository siteRepository, IMapper mapper, ICacheService cacheService)
        {
            _siteRepository = siteRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<PagedResult<SiteDTO>> Handle(GetSitesQuery request, CancellationToken cancellationToken)
        {
            var filter = request.PagingFilter;
            string cacheKey = $"sites:{filter.PageNumber}:{filter.PageSize}:{filter.Search}";

            var cached = _cacheService.Get<PagedResult<SiteDTO>>(cacheKey);
            if (cached != null)
                return cached;

            var query = _siteRepository.Query();

            if (!string.IsNullOrWhiteSpace(filter.Search))
                query = query.Where(s => s.Name.Contains(filter.Search));

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

            _cacheService.Set(cacheKey, result, TimeSpan.FromMinutes(CacheConstants.CacheDurationInMinutes));

            return result;
        }
    }
}