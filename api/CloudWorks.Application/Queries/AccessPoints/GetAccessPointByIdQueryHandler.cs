using AutoMapper;
using CloudWorks.Application.DTOs.AccessPoints;
using CloudWorks.Application.Exceptions;
using CloudWorks.Services.Contracts.AccessPoints;
using MediatR;

namespace CloudWorks.Application.Queries.AccessPoints
{
    public class GetAccessPointByIdQueryHandler : IRequestHandler<GetAccessPointByIdQuery, AccessPointDTO>
    {
        private readonly IAccessPointRepository _accessPointRepository;
        private readonly IMapper _mapper;

        public GetAccessPointByIdQueryHandler(
            IAccessPointRepository accessPointRepository,
            IMapper mapper)
        {
            _accessPointRepository = accessPointRepository;
            _mapper = mapper;
        }

        public async Task<AccessPointDTO> Handle(GetAccessPointByIdQuery request, CancellationToken cancellationToken)
        {
            var accessPoint = await _accessPointRepository.GetByIdAsync(request.Id, cancellationToken);

            if (accessPoint == null)
            {
                throw new NotFoundException($"Access point with ID {request.Id} not found.");
            }

            return _mapper.Map<AccessPointDTO>(accessPoint);
        }
    }
}
