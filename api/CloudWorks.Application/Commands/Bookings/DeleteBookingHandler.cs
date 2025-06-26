using CloudWorks.Services.Contracts.Bookings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Commands.Bookings
{
    public class DeleteBookingHandler : IRequestHandler<DeleteBookingCommand>
    {
        private readonly IBookingRepository _repository;

        public DeleteBookingHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
            await _repository.SaveChangesAsync(cancellationToken);         
        }
    }
}
