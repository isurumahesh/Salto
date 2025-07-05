using CloudWorks.Application.DTOs.AccessPoints;
using MediatR;

namespace CloudWorks.Application.Queries.AccessPoints
{
    public record GetAccessPointByIdQuery(Guid Id) : IRequest<AccessPointDTO>;
}