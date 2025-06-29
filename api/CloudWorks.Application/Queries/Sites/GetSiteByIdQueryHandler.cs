using AutoMapper;
using CloudWorks.Application.DTOs.Sites;
using CloudWorks.Services.Contracts.Sites;
using MediatR;

namespace CloudWorks.Application.Queries.Sites
{
    public class GetSiteByIdQueryHandler : IRequestHandler<GetSiteByIdQuery, SiteDTO>
    {
        private readonly ISiteRepository _repository;
        private readonly IMapper _mapper;

        public GetSiteByIdQueryHandler(ISiteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SiteDTO> Handle(GetSiteByIdQuery request, CancellationToken cancellationToken)
        {
            var site = await _repository.GetByIdAsync(request.Id);
            if (site is null)
                return null;

            return _mapper.Map<SiteDTO>(site);
        }
    }
}