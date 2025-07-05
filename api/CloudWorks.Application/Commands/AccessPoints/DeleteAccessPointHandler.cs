using CloudWorks.Application.Exceptions;
using CloudWorks.Services.Contracts.AccessPoints;
using MediatR;

namespace CloudWorks.Application.Commands.AccessPoints
{
    public class DeleteAccessPointHandler : IRequestHandler<DeleteAccessPointCommand>
    {
        private readonly IAccessPointRepository _repository;

        public DeleteAccessPointHandler(IAccessPointRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteAccessPointCommand request, CancellationToken cancellationToken)
        {
            var accessPoint = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (accessPoint is null)
            {
                throw new NotFoundException($"Access Point with ID {request.Id} not found.");
            }

            await _repository.DeleteAsync(accessPoint, cancellationToken);
        }
    }
}