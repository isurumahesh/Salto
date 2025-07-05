using CloudWorks.Application.Commands.Bookings;
using CloudWorks.Application.DTOs.Bookings;
using CloudWorks.Application.DTOs.Pagination;
using CloudWorks.Application.Queries.Bookings;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudWorks.Api.Endpoints;

[ApiController]
[Route("sites/{siteId:guid}/bookings")]
public class BookingsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<AddBookingDTO> _addValidator;

    public BookingsController(IMediator mediator, IValidator<AddBookingDTO> addValidator)
    {
        _mediator = mediator;
        _addValidator = addValidator;
    }

    [HttpPost]
    [Authorize(Policy = "UserAccess")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddBooking([FromRoute] Guid siteId, AddBookingDTO request, CancellationToken cancellationToken)
    {
        ValidationResult result = await _addValidator.ValidateAsync(request);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        var booking = await _mediator.Send(new AddBookingCommand(siteId, request), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { siteId = siteId, bookingId = booking.Id }, booking);
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(PagedResult<BookingDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList([FromQuery] PagingFilter pagingFilter, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBookingsQuery(pagingFilter), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{bookingId:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(PagedResult<BookingDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid bookingId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBookingByIdQuery(bookingId), cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{bookingId:guid}")]
    [Authorize(Policy = "UserAccess")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid bookingId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteBookingCommand(bookingId), cancellationToken);
        return NoContent();
    }
}