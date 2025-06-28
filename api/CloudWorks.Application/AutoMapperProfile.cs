using CloudWorks.Application.DTOs.AccessPoints;
using CloudWorks.Application.DTOs.Profiles;
using CloudWorks.Application.DTOs.SiteProfiles;
using CloudWorks.Application.DTOs.Sites;
using CloudWorks.Data.Contracts.Entities;

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
            CreateMap<AccessPoint, AccessPointDTO>();
            CreateMap<Site, SiteDTO>().ReverseMap();
            CreateMap<AddProfileDTO, Profile>();
            CreateMap<AddSiteProfileDTO, SiteProfile>();
        }
    }
}