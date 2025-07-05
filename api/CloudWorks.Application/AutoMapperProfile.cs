using CloudWorks.Application.DTOs.AccessEvents;
using CloudWorks.Application.DTOs.AccessPoints;
using CloudWorks.Application.DTOs.Bookings;
using CloudWorks.Application.DTOs.Profiles;
using CloudWorks.Application.DTOs.Schedules;
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
            CreateMap<AccessPoint, AccessPointDTO>().ReverseMap();
            CreateMap<Site, SiteDTO>().ReverseMap();
            CreateMap<AddProfileDTO, Profile>();
            CreateMap<Profile, ProfileDTO>();
            CreateMap<AddSiteProfileDTO, SiteProfile>();
            CreateMap<AccessEvent, AccessEventDTO>()
            .ForMember(dest => dest.AccessPointName, opt => opt.MapFrom(src => src.AccessPoint!.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Profile!.Email));

            CreateMap<SiteProfile, SiteProfileDTO>();
            CreateMap<Schedule, ScheduleDTO>();
            CreateMap<Booking, BookingDTO>();
        }
    }
}