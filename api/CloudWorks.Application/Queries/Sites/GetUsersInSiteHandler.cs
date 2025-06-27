using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.Sites;
using MediatR;

namespace CloudWorks.Application.Queries.Sites
{
    public class GetUsersInSiteHandler : IRequestHandler<GetUsersInSiteQuery, IEnumerable<Profile>>
    {
        private readonly ISiteRepository _repository;

        public GetUsersInSiteHandler(ISiteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Profile>> Handle(GetUsersInSiteQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetUsersInSiteAsync(request.SiteId);
        }
    }
}