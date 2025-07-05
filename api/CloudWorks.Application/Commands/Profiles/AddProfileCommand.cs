using CloudWorks.Application.DTOs.Profiles;
using CloudWorks.Data.Contracts.Entities;
using MediatR;

namespace CloudWorks.Application.Commands.Profiles
{
    public record AddProfileCommand(AddProfileDTO AddProfileDTO) : IRequest<Profile>;
}