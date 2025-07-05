using AutoMapper;
using CloudWorks.Application.DTOs.Profiles;
using CloudWorks.Application.Exceptions;
using CloudWorks.Services.Contracts.Profiles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Queries.Profiles
{
    public class GetProfileByIdQueryHandler : IRequestHandler<GetProfileByIdQuery, ProfileDTO>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public GetProfileByIdQueryHandler(IProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        public async Task<ProfileDTO> Handle(GetProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var profile = await _profileRepository.GetByIdAsync(request.Id, cancellationToken);

            if (profile is null)
            {
                throw new NotFoundException($"Profile with Id {request.Id} not found.");
            }

            return _mapper.Map<ProfileDTO>(profile);
        }
    }
}
