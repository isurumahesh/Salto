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
    public class GetSiteProfileByProfileIdQueryHandler : IRequestHandler<GetSiteProfileByProfileIdQuery, List<SiteProfileDTO>>
    {
        private readonly ISiteProfileRepository _repository;
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public GetSiteProfileByProfileIdQueryHandler(ISiteProfileRepository repository, IProfileRepository profileRepository, IMapper mapper)
        {
            _repository = repository;
            _profileRepository = profileRepository;
            _mapper=mapper;
        }

        public async Task<List<SiteProfileDTO>> Handle(GetSiteProfileByProfileIdQuery request, CancellationToken cancellationToken)
        {
            var site = await _profileRepository.GetByIdAsync(request.ProfileId, cancellationToken);
            if (site is null)
            {
                throw new NotFoundException($"Profile with Id {request.ProfileId} not found.");
            }
            var profiles= await _repository.GetByProfileIdAsync(request.ProfileId, cancellationToken);
            return _mapper.Map<List<SiteProfileDTO>>(profiles);
        }
    }
}