using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.AccessEvents;
using CloudWorks.Services.Contracts.AccessPoints;
using CloudWorks.Services.Contracts.Bookings;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CloudWorks.Application.Commands.AccessPoints
{
    public class AttemptAccessPointHandler : IRequestHandler<AttemptAccessPointCommand, AccessPointCommandResult<OpenAccessPointCommand>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IAccessEventRepository _accessEventRepository;
        private readonly ILogger<AttemptAccessPointHandler> _logger;

        public AttemptAccessPointHandler(IBookingRepository bookingRepository, IAccessEventRepository accessEventRepository, ILogger<AttemptAccessPointHandler> logger)
        {
            _bookingRepository = bookingRepository;
            _accessEventRepository = accessEventRepository;
            _logger = logger;
        }

        public async Task<AccessPointCommandResult<OpenAccessPointCommand>> Handle(
            AttemptAccessPointCommand command,
            CancellationToken cancellationToken)
        {
            var errorMessage = string.Empty;
            bool hasValidBooking = false;
            try
            {
                hasValidBooking = await _bookingRepository.HasValidBookingAsync(
                        command.OpenAccessPointCommand.ProfileId,
                        command.OpenAccessPointCommand.AccessPointId,
                        command.SiteId,
                        command.Now,
                        cancellationToken
                    );
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                this._logger.LogError(ex, "Error checking booking for access point {AccessPointId} at site {SiteId}",
                    command.OpenAccessPointCommand.AccessPointId, command.SiteId);
            }

            await _accessEventRepository.AddAsync(new AccessEvent
            {
                Id = Guid.NewGuid(),
                AccessPointId = command.OpenAccessPointCommand.AccessPointId,
                SiteId = command.SiteId,
                ProfileId = command.OpenAccessPointCommand.ProfileId,
                Success = hasValidBooking,
                Timestamp = command.Now,
                Reason = hasValidBooking ? "Valid booking" : $"Access denied: {errorMessage ?? "No valid booking"}"
            });

            await _accessEventRepository.SaveChangesAsync(cancellationToken);

            var result = new AccessPointCommandResult<OpenAccessPointCommand>()
            {
                Command = command.OpenAccessPointCommand,
                TimeStamp = command.Now,
                Success = hasValidBooking,
                Error = !hasValidBooking ? errorMessage : null
            };

            return result;
        }
    }
}