using AutoMapper;
using CloudWorks.Services.Contracts.Sites;
using MediatR;

namespace CloudWorks.Application.Commands.Sites
{
    public class AddSiteHandler : IRequestHandler<AddSiteCommand, Guid>
    {
        private readonly ISiteRepository _repository;
        private readonly IMapper _mapper;

        public AddSiteHandler(ISiteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddSiteCommand request, CancellationToken cancellationToken)
        {
            var site = _mapper.Map<Site>(request.AddSiteDTO);
            await _repository.AddAsync(site);
            await _repository.SaveChangesAsync(cancellationToken);
            return site.Id;
        }
    }
}