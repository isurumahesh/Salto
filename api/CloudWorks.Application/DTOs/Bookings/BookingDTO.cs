using CloudWorks.Application.DTOs.AccessPoints;
using CloudWorks.Application.DTOs.Schedules;
using CloudWorks.Application.DTOs.SiteProfiles;
using CloudWorks.Application.DTOs.Sites;

namespace CloudWorks.Application.DTOs.Bookings
{
    public class BookingDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public SiteDTO? Site { get; set; }

        public ICollection<SiteProfileDTO> SiteProfiles { get; set; } = [];
        public ICollection<ScheduleDTO> Schedules { get; set; } = [];
        public ICollection<AccessPointDTO> AccessPoints { get; set; } = [];
    }
}