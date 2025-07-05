using CloudWorks.Application.DTOs.Profiles;
using MediatR;

namespace CloudWorks.Application.Queries.Profiles
{
    public record GetProfileByIdQuery(Guid Id) : IRequest<ProfileDTO>;
}