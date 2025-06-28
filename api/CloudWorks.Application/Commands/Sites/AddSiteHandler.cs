using AutoMapper;
using CloudWorks.Application.DTOs.Sites;
using CloudWorks.Services.Contracts.Sites;
using MediatR;

namespace CloudWorks.Application.Commands.Sites
{
    public class AddSiteHandler : IRequestHandler<AddSiteCommand, SiteDTO>
    {
        private readonly ISiteRepository _repository;
        private readonly IMapper _mapper;

        public AddSiteHandler(ISiteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SiteDTO> Handle(AddSiteCommand request, CancellationToken cancellationToken)
        {
            var site = _mapper.Map<Site>(request.AddSiteDTO);
            await _repository.AddAsync(site);
            return _mapper.Map<SiteDTO>(site);
        }
    }
}