using AutoMapper;
using CloudWorks.Services.Contracts.AccessPoints;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var existingAccessPoint = await _repository.GetByIdAsync(request.UpdateAccessPointDTO.Id);
            var accessPoint = _mapper.Map(request.UpdateAccessPointDTO, existingAccessPoint);

            await _repository.UpdateAsync(accessPoint);
            await _repository.SaveChangesAsync(cancellationToken);

        }
    }
}
