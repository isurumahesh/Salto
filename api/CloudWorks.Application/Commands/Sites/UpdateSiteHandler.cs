using AutoMapper;
using CloudWorks.Services.Contracts.Sites;
using MediatR;

namespace CloudWorks.Application.Commands.Sites
{
    public class UpdateSiteHandler : IRequestHandler<UpdateSiteCommand>
    {
        private readonly ISiteRepository _repository;
        private readonly IMapper _mapper;

        public UpdateSiteHandler(ISiteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateSiteCommand request, CancellationToken cancellationToken)
        {
            var existingSite = await _repository.GetByIdAsync(request.UpdateSiteDTO.Id);
            var site = _mapper.Map(request.UpdateSiteDTO, existingSite);
            await _repository.UpdateAsync(site);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}