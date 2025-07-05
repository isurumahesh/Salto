using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.SiteProfiles;
using MediatR;

namespace CloudWorks.Application.Queries.SiteProfiles
{
    public class GetSiteProfileByProfileIdQueryHandler : IRequestHandler<GetSiteProfileByProfileIdQuery, SiteProfile>
    {
        private readonly ISiteProfileRepository _repository;

        public GetSiteProfileByProfileIdQueryHandler(ISiteProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<SiteProfile> Handle(GetSiteProfileByProfileIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByProfileIdAsync(request.ProfileId, cancellationToken);
        }
    }
}