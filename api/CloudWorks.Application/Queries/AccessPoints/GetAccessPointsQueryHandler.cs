using AutoMapper;
using CloudWorks.Application.DTOs.AccessPoints;
using CloudWorks.Application.DTOs.Pagination;
using CloudWorks.Data.Contracts.Models;
using CloudWorks.Services.Contracts.AccessPoints;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CloudWorks.Application.Queries.AccessPoints
{
    public class GetAccessPointsQueryHandler : IRequestHandler<GetAccessPointsQuery, PagedResult<AccessPointDTO>>
    {
        private readonly IAccessPointRepository _repository;
        private readonly IMapper _mapper;

        public GetAccessPointsQueryHandler(IAccessPointRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResult<AccessPointDTO>> Handle(GetAccessPointsQuery request, CancellationToken cancellationToken)
        {
            var filter = request.PagingFilter;
            var query = _repository.QueryBySiteId(request.SiteId);

            if (!string.IsNullOrWhiteSpace(filter.Search))
                query = query.Where(ap => ap.Name.Contains(filter.Search));

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(ap => ap.Name)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<AccessPointDTO>
            {
                Items = _mapper.Map<List<AccessPointDTO>>(items),
                TotalCount = totalCount,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }
    }
}