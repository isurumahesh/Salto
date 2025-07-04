﻿using CloudWorks.Application.DTOs.Schedules;
using MediatR;

namespace CloudWorks.Application.Queries.Schedules
{
    public record GetFreeTimeSlotsQuery(List<Guid> AccessPointIds, DateTime Start, DateTime End, CancellationToken CancellationToken) : IRequest<List<AccessPointTimeSlotDto>>;
}