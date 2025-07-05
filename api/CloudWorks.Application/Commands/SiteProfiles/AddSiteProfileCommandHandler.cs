using AutoMapper;
using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.SiteProfiles;
using MediatR;

namespace CloudWorks.Application.Commands.SiteProfiles
{
    public class AddSiteProfileCommandHandler : IRequestHandler<AddSiteProfileCommand, SiteProfile>
    {
        private readonly ISiteProfileRepository _repository;
        private readonly IMapper _mapper;

        public AddSiteProfileCommandHandler(ISiteProfileRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SiteProfile> Handle(AddSiteProfileCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<SiteProfile>(request.Dto);
            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}