using CloudWorks.Application.Exceptions;
using CloudWorks.Services.Contracts.Bookings;
using MediatR;

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
            var booking = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (booking is null)
            {
                throw new NotFoundException($"Booking with ID {request.Id} not found.");
            }

            await _repository.DeleteAsync(booking, cancellationToken);

        }
    }
}