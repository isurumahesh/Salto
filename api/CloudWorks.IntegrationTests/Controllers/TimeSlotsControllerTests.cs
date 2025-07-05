using CloudWorks.Application.DTOs.Schedules;
using CloudWorks.Application.DTOs.TimeSlots;
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
    public class TimeSlotsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public TimeSlotsControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        }

        [Fact]
        public async Task GetFreeTimeSlots_ReturnsOk_WithValidInput()
        {
            // Arrange
            var request = new GetFreeTimeSlotsRequestDTO
            {
                AccessPointIds = new List<Guid> { TestData.AccessPointId },
                Start = DateTime.UtcNow,
                End = DateTime.UtcNow.AddHours(1)
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/timeslots/free", request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<List<AccessPointTimeSlotDto>>();
            Assert.NotNull(result);
            Assert.All(result, dto =>
            {
                Assert.Equal(TestData.AccessPointId, dto.AccessPointId);
                Assert.NotNull(dto.TimeSlots);
            });
        }

        [Fact]
        public async Task GetUserContinuousAccess_ReturnsOk_WithValidInput()
        {
            // Arrange
            var request = new GetContinuousTimeSlotsRequestDTO
            {
                ProfileId = TestData.ProfileId,
                AccessPointIds = new List<Guid> { TestData.AccessPointId },
                Start = DateTime.UtcNow,
                End = DateTime.UtcNow.AddHours(1)
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/timeslots/continuous-access", request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<List<AccessPointTimeSlotDto>>();
            Assert.NotNull(result);
            Assert.All(result, dto =>
            {
                Assert.Equal(TestData.AccessPointId, dto.AccessPointId);
            });
        }

    }
}
