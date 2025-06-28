using AutoMapper;
using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.AccessPoints;
using MediatR;

namespace CloudWorks.Application.Commands.AccessPoints
{
    public class AddAccessPointHandler : IRequestHandler<AddAccessPointCommand, Guid>
    {
        private readonly IAccessPointRepository _repository;
        private readonly IMapper _mapper;

        public AddAccessPointHandler(IAccessPointRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddAccessPointCommand request, CancellationToken cancellationToken)
        {
            var accessPoint = _mapper.Map<AccessPoint>(request.AddAccessPointDTO);
            await _repository.AddAsync(accessPoint);
            return accessPoint.Id;
        }
    }
}