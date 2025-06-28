using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.SiteProfiles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
