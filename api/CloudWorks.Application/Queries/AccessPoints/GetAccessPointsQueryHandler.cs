using AutoMapper;
using CloudWorks.Application.DTOs.AccessPoints;
using CloudWorks.Services.Contracts.AccessPoints;
using MediatR;

namespace CloudWorks.Application.Queries.AccessPoints
{
    public class GetAccessPointsQueryHandler : IRequestHandler<GetAccessPointsQuery, List<AccessPointDTO>>
    {
        private readonly IAccessPointRepository _repository;
        private readonly IMapper _mapper;

        public GetAccessPointsQueryHandler(IAccessPointRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<AccessPointDTO>> Handle(GetAccessPointsQuery request, CancellationToken cancellationToken)
        {
            var accessPoints = await _repository.GetBySiteIdAsync(request.SiteId);
            return _mapper.Map<List<AccessPointDTO>>(accessPoints);
        }
    }
}