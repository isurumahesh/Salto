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
            await _repository.DeleteAsync(request.Id);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}