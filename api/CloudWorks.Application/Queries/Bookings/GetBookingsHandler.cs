using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.Bookings;
using MediatR;

namespace CloudWorks.Application.Queries.Bookings
{
    public class GetBookingsHandler : IRequestHandler<GetBookingsQuery, IEnumerable<Booking>>
    {
        private readonly IBookingRepository _repository;

        public GetBookingsHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Booking>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}