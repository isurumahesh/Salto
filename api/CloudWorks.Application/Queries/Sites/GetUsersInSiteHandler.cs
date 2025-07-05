using CloudWorks.Application.Exceptions;
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
            var site = await _repository.GetByIdAsync(request.SiteId, cancellationToken);
            if (site is null)
            {
                throw new NotFoundException($"Site with ID {request.SiteId} not found.");
            }

            return await _repository.GetUsersInSiteAsync(request.SiteId, cancellationToken);
        }
    }
}