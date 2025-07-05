using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.IntegrationTests.Configuration
{
    public static class TestData
    {
        public static Guid SiteId { get; private set; }
        public static Guid ProfileId { get; private set; }
        public static Guid AccessPointId { get; private set; }
        public static Guid BookingId { get; private set; }
        public static Guid SiteProfileId { get; private set; }

        internal static void Set(Guid siteId, Guid profileId, Guid accessPointId, Guid bookingId, Guid siteProfileId)
        {
            SiteId = siteId;
            ProfileId = profileId;
            AccessPointId = accessPointId;
            BookingId = bookingId;
            SiteProfileId = siteProfileId;
        }
    }
}
