using AutoMapper;
using CloudWorks.Application.Exceptions;
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
            if (existingSite == null)
            {
                throw new NotFoundException($"Site with ID {request.UpdateSiteDTO.Id} not found.");
            }

            var site = _mapper.Map(request.UpdateSiteDTO, existingSite);
            await _repository.UpdateAsync(site);
        }
    }
}