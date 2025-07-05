using AutoMapper;
using CloudWorks.Application.DTOs.SiteProfiles;
using CloudWorks.Application.Exceptions;
using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.Profiles;
using CloudWorks.Services.Contracts.SiteProfiles;
using CloudWorks.Services.Contracts.Sites;
using MediatR;

namespace CloudWorks.Application.Queries.SiteProfiles
{
    public class GetSiteProfileByIdQueryHandler : IRequestHandler<GetSiteProfileByIdQuery, SiteProfileDTO>
    {
        private readonly ISiteProfileRepository _repository;       
        private readonly IMapper _mapper;

        public GetSiteProfileByIdQueryHandler(ISiteProfileRepository repository,IMapper mapper)
        {
            _repository = repository;           
            _mapper=mapper;
        }

        public async Task<SiteProfileDTO> Handle(GetSiteProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var siteProfile = await _repository.GetByIdAsync(request.SiteProfileId, cancellationToken);
            if (siteProfile is null)
            {
                throw new NotFoundException($"Site Profile with Id {request.SiteProfileId} not found.");
            }
           
            return _mapper.Map<SiteProfileDTO>(siteProfile);
        }
    }
}