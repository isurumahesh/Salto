using AutoMapper;
using CloudWorks.Application.DTOs.Bookings;
using CloudWorks.Application.DTOs.Pagination;
using CloudWorks.Application.DTOs.Sites;
using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.Bookings;
using CloudWorks.Services.Contracts.Sites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CloudWorks.Application.Queries.Bookings
{
    public class GetBookingsHandler : IRequestHandler<GetBookingsQuery, PagedResult<BookingDTO>>
    {
        private readonly IBookingRepository _repository;
        private readonly IMapper _mapper;

        public GetBookingsHandler(IBookingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResult<BookingDTO>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
        {
            var filter = request.PagingFilter;

            var query = _repository.Query();

            if (!string.IsNullOrWhiteSpace(filter.Search))
                query = query.Where(s => s.Name.Contains(filter.Search));

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(s => s.Name)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync(cancellationToken);

            var result = new PagedResult<BookingDTO>
            {
                Items = _mapper.Map<List<BookingDTO>>(items),
                TotalCount = totalCount,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };

            return result;
        }
    }
}