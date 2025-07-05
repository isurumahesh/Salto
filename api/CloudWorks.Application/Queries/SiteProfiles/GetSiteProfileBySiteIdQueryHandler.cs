using AutoMapper;
using CloudWorks.Application.DTOs.SiteProfiles;
using CloudWorks.Application.Exceptions;
using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.SiteProfiles;
using CloudWorks.Services.Contracts.Sites;
using MediatR;

namespace CloudWorks.Application.Queries.SiteProfiles
{
    public class GetSiteProfileBySiteIdQueryHandler : IRequestHandler<GetSiteProfileBySiteIdQuery, List<SiteProfileDTO>>
    {
        private readonly ISiteProfileRepository _repository;
        private readonly ISiteRepository _siteRepository;
        private readonly IMapper _mapper;

        public GetSiteProfileBySiteIdQueryHandler(ISiteProfileRepository repository, ISiteRepository siteRepository, IMapper mapper)
        {
            _repository = repository;
            _siteRepository = siteRepository;
            _mapper = mapper;
        }

        public async Task<List<SiteProfileDTO>> Handle(GetSiteProfileBySiteIdQuery request, CancellationToken cancellationToken)
        {
            var site = await _siteRepository.GetByIdAsync(request.SiteId, cancellationToken);
            if (site is null)
            {
                throw new NotFoundException($"Site with Id {request.SiteId} not found.");
            }
            var profiles = await _repository.GetBySiteIdAsync(request.SiteId, cancellationToken);

            return _mapper.Map<List<SiteProfileDTO>>(profiles);
        }
    }
}