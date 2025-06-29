using AutoMapper;
using CloudWorks.Application.Constants;
using CloudWorks.Application.DTOs.AccessPoints;
using CloudWorks.Application.DTOs.Pagination;
using CloudWorks.Services.Contracts.AccessPoints;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CloudWorks.Application.Queries.AccessPoints
{
    public class GetAccessPointsQueryHandler : IRequestHandler<GetAccessPointsQuery, PagedResult<AccessPointDTO>>
    {
        private readonly IAccessPointRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetAccessPointsQueryHandler(IAccessPointRepository repository, ICacheService cacheService, IMapper mapper)
        {
            _repository = repository;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        public async Task<PagedResult<AccessPointDTO>> Handle(GetAccessPointsQuery request, CancellationToken cancellationToken)
        {
            var filter = request.PagingFilter;

            string cacheKey = $"accesspoints:{request.SiteId}:{filter.PageNumber}:{filter.PageSize}:{filter.Search}";

            var cached = _cacheService.Get<PagedResult<AccessPointDTO>>(cacheKey);
            if (cached != null)
                return cached;

            var query = _repository.QueryBySiteId(request.SiteId);

            if (!string.IsNullOrWhiteSpace(filter.Search))
                query = query.Where(ap => ap.Name.Contains(filter.Search));

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(ap => ap.Name)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync(cancellationToken);

            var result = new PagedResult<AccessPointDTO>
            {
                Items = _mapper.Map<List<AccessPointDTO>>(items),
                TotalCount = totalCount,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };

            _cacheService.Set(cacheKey, result, TimeSpan.FromMinutes(CacheConstants.CacheDurationInMinutes));

            return result;
        }
    }
}