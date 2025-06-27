using AutoMapper;
using CloudWorks.Application.DTOs.Sites;
using CloudWorks.Data.Contracts.Models;
using CloudWorks.Services.Contracts.Sites;
using MediatR;

namespace CloudWorks.Application.Queries.Sites
{
    public class GetSitesQueryHandler : IRequestHandler<GetSitesQuery, PagedResult<SiteDTO>>
    {
        private readonly ISiteRepository _siteRepository;
        private readonly IMapper _mapper;

        public GetSitesQueryHandler(ISiteRepository siteRepository, IMapper mapper)
        {
            _siteRepository = siteRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<SiteDTO>> Handle(GetSitesQuery request, CancellationToken cancellationToken)
        {
            var pagedSites = await _siteRepository.GetSitesAsync(
                request.PageNumber,
                request.PageSize,
                request.NameFilter,
                cancellationToken);

            var dtoItems = _mapper.Map<List<SiteDTO>>(pagedSites.Items);

            return new PagedResult<SiteDTO>
            {
                Items = dtoItems,
                TotalCount = pagedSites.TotalCount,
                PageNumber = pagedSites.PageNumber,
                PageSize = pagedSites.PageSize
            };
        }
    }
}