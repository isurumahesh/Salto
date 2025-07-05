using AutoMapper;
using CloudWorks.Application.DTOs.Profiles;
using CloudWorks.Services.Contracts.Profiles;
using MediatR;

namespace CloudWorks.Application.Queries.Profiles
{
    public class GetProfilesQueryHandler : IRequestHandler<GetProfilesQuery, List<ProfileDTO>>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public GetProfilesQueryHandler(IProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        public async Task<List<ProfileDTO>> Handle(GetProfilesQuery request, CancellationToken cancellationToken)
        {
            var profiles = await _profileRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<ProfileDTO>>(profiles);
        }
    }
}