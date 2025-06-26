using AutoMapper;
using CloudWorks.Application.DTOs.AccessPoints;
using CloudWorks.Application.DTOs.Sites;
using CloudWorks.Data.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddSiteDTO, Site>();
            CreateMap<UpdateSiteDTO, Site>();
            CreateMap<UpdateAccessPointDTO, AccessPoint>();
            CreateMap<AddAccessPointDTO, AccessPoint>();
        }
    }
}
