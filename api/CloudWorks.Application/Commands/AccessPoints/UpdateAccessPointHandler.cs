using AutoMapper;
using CloudWorks.Application.Exceptions;
using CloudWorks.Services.Contracts.AccessPoints;
using MediatR;

namespace CloudWorks.Application.Commands.AccessPoints
{
    public class UpdateAccessPointHandler : IRequestHandler<UpdateAccessPointCommand>
    {
        private readonly IAccessPointRepository _repository;
        private readonly IMapper _mapper;

        public UpdateAccessPointHandler(IAccessPointRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateAccessPointCommand request, CancellationToken cancellationToken)
        {
            var existingAccessPoint = await _repository.GetByIdAsync(request.UpdateAccessPointDTO.Id, cancellationToken);
            if (existingAccessPoint is null)
            {
                throw new NotFoundException($"Access Point with ID {request.UpdateAccessPointDTO.Id} not found.");
            }

            var accessPoint = _mapper.Map(request.UpdateAccessPointDTO, existingAccessPoint);

            await _repository.UpdateAsync(accessPoint, cancellationToken);
        }
    }
}