using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.SiteProfiles;
using MediatR;

namespace CloudWorks.Application.Queries.SiteProfiles
{
    public class GetSiteProfileBySiteIdQueryHandler : IRequestHandler<GetSiteProfileBySiteIdQuery, List<SiteProfile>>
    {
        private readonly ISiteProfileRepository _repository;

        public GetSiteProfileBySiteIdQueryHandler(ISiteProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SiteProfile>> Handle(GetSiteProfileBySiteIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetBySiteIdAsync(request.SiteId, cancellationToken);
        }
    }
}