using CloudWorks.Application.DTOs.Profiles;
using CloudWorks.Data.Contracts.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Commands.Profiles
{
    public record AddProfileCommand(AddProfileDTO AddProfileDTO) : IRequest<Profile>;
}
