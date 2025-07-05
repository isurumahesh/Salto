using CloudWorks.Application.Exceptions;
using CloudWorks.Services.Contracts.Sites;
using MediatR;

namespace CloudWorks.Application.Commands.Sites
{
    public class DeleteSiteHandler : IRequestHandler<DeleteSiteCommand>
    {
        private readonly ISiteRepository _repository;

        public DeleteSiteHandler(ISiteRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteSiteCommand request, CancellationToken cancellationToken)
        {
            var existingSite = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (existingSite == null)
            {
                throw new NotFoundException($"Site with ID {request.Id} not found.");
            }

            await _repository.DeleteAsync(existingSite, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}