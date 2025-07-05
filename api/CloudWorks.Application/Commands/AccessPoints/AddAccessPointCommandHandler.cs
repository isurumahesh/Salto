using AutoMapper;
using CloudWorks.Application.DTOs.AccessPoints;
using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.AccessPoints;
using MediatR;

namespace CloudWorks.Application.Commands.AccessPoints
{
    public class AddAccessPointCommandHandler : IRequestHandler<AddAccessPointCommand, AccessPointDTO>
    {
        private readonly IAccessPointRepository _repository;
        private readonly IMapper _mapper;

        public AddAccessPointCommandHandler(IAccessPointRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AccessPointDTO> Handle(AddAccessPointCommand request, CancellationToken cancellationToken)
        {
            var accessPoint = _mapper.Map<AccessPoint>(request.AddAccessPointDTO);
            await _repository.AddAsync(accessPoint);
            return _mapper.Map<AccessPointDTO>(accessPoint);
        }
    }
}