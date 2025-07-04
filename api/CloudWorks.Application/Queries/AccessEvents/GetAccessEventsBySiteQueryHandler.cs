using AutoMapper;
using CloudWorks.Application.DTOs.AccessEvents;
using CloudWorks.Services.Contracts.AccessEvents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Queries.AccessEvents
{
    public class GetAccessEventsBySiteQueryHandler : IRequestHandler<GetAccessEventsBySiteQuery, List<AccessEventDTO>>
    {
        private readonly IAccessEventRepository _repository;
        private readonly IMapper _mapper;

        public GetAccessEventsBySiteQueryHandler(IAccessEventRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<AccessEventDTO>> Handle(GetAccessEventsBySiteQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetBySiteIdAsync(request.SiteId, cancellationToken);
            return _mapper.Map<List<AccessEventDTO>>(entities);
        }
    }
}
