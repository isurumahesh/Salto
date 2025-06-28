using AutoMapper;
using CloudWorks.Services.Contracts.Profiles;
using MediatR;
using Profile = CloudWorks.Data.Contracts.Entities.Profile;

namespace CloudWorks.Application.Commands.Profiles
{
    public class AddProfileCommandHandler : IRequestHandler<AddProfileCommand, Profile>
    {
        private readonly IProfileRepository _repository;
        private readonly IMapper _mapper;

        public AddProfileCommandHandler(IProfileRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Profile> Handle(AddProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = _mapper.Map<Profile>(request.AddProfileDTO);
            await _repository.AddAsync(profile, cancellationToken);
            return profile;
        }
    }
}