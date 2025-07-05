using AutoMapper;
using CloudWorks.Application.DTOs.Bookings;
using CloudWorks.Application.Exceptions;
using CloudWorks.Services.Contracts.Bookings;
using MediatR;

namespace CloudWorks.Application.Queries.Bookings
{
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingDTO>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public GetBookingByIdQueryHandler(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<BookingDTO> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetByIdAsync(request.Id, cancellationToken);

            if (booking == null)
            {
                throw new NotFoundException($"Booking with ID {request.Id} not found.");
            }

            return _mapper.Map<BookingDTO>(booking);
        }
    }
}