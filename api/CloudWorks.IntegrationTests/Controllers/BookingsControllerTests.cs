using CloudWorks.Application.DTOs.Bookings;
using CloudWorks.Application.DTOs.Pagination;
using CloudWorks.Application.DTOs.Schedules;
using CloudWorks.IntegrationTests.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.IntegrationTests.Controllers
{
    public class BookingsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public BookingsControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        }

        [Fact]
        public async Task GetList_ReturnsPagedResult()
        {
            var response = await _httpClient.GetAsync($"/sites/{TestData.SiteId}/bookings?PageNumber=1&PageSize=10");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var pagedResult = await response.Content.ReadFromJsonAsync<PagedResult<BookingDTO>>();
            Assert.NotNull(pagedResult);
            Assert.NotEmpty(pagedResult.Items);
        }

        [Fact]
        public async Task GetById_ReturnsBooking_WhenExists()
        {
            var response = await _httpClient.GetAsync($"/sites/{TestData.SiteId}/bookings/{TestData.BookingId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var booking = await response.Content.ReadFromJsonAsync<BookingDTO>();
            Assert.NotNull(booking);
            Assert.Equal(TestData.BookingId, booking.Id);
        }

        [Fact]
        public async Task AddBooking_ReturnsCreated_WhenValid()
        {
            var dto = new AddBookingDTO
            {
                Name = "Test Booking",
                AccessPoints = new List<Guid> { TestData.AccessPointId },
                SiteProfiles = new List<Guid> { TestData.SiteProfileId },
                Schedules = new List<ScheduleRequestDTO>
            {
                new ScheduleRequestDTO
                {
                    StartUtc = DateTime.UtcNow.AddMinutes(10),
                    EndUtc = DateTime.UtcNow.AddMinutes(60),                   
                }
            }
            };

            var response = await _httpClient.PostAsJsonAsync($"/sites/{TestData.SiteId}/bookings", dto);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var created = await response.Content.ReadFromJsonAsync<BookingDTO>();
            Assert.NotNull(created);
            Assert.Equal(dto.Name, created.Name);
        }

        [Fact]
        public async Task AddBooking_ReturnsBadRequest_WhenValidationFails()
        {
            var dto = new AddBookingDTO
            {
                Name = "", // invalid
                AccessPoints = new List<Guid>(), // invalid
                SiteProfiles = new List<Guid>(), // invalid
                Schedules = new List<ScheduleRequestDTO>()
            };

            var response = await _httpClient.PostAsJsonAsync($"/sites/{TestData.SiteId}/bookings", dto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenBookingExists()
        {
            var response = await _httpClient.DeleteAsync($"/sites/{TestData.SiteId}/bookings/{TestData.BookingId}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
