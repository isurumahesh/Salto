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
            await _repository.DeleteAsync(request.Id);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}