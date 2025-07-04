using CloudWorks.Application.Queries.AccessEvents;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudWorks.Api.Endpoints
{
    [Route("sites/{siteId:guid}/accessEvents")]
    [ApiController]
    public class AccessEventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccessEventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccessEventsForSite([FromRoute]Guid siteId)
        {
            var events = await _mediator.Send(new GetAccessEventsBySiteQuery(siteId));
            return Ok(events);
        }
    }
}
