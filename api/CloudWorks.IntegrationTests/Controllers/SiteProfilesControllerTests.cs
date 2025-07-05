using CloudWorks.Application.DTOs.SiteProfiles;
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
    public class SiteProfilesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public SiteProfilesControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
            _httpClient.DefaultRequestHeaders.Add("scopes", "scwapi:use");
            _httpClient.DefaultRequestHeaders.Add("roles", "Administrator");
        }

        [Fact]
        public async Task AddSiteProfile_ReturnsCreated_WhenValidInput()
        {
            // Arrange
            var dto = new AddSiteProfileDTO
            {
                SiteId = TestData.SiteId,
                ProfileId = TestData.ProfileId
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/siteprofiles", dto);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<SiteProfileDTO>();
            Assert.NotNull(result);
            Assert.Equal(dto.SiteId, result.SiteId);
            Assert.Equal(dto.ProfileId, result.ProfileId);
        }

        [Fact]
        public async Task GetByProfileId_ReturnsOk_WhenExists()
        {
            // Arrange
            var url = $"/profiles/{TestData.ProfileId}/siteprofiles";

            // Act
            var response = await _httpClient.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<List<SiteProfileDTO>>();
            Assert.NotNull(result);
            Assert.Contains(result, sp => sp.ProfileId == TestData.ProfileId);
        }

        [Fact]
        public async Task GetBySiteId_ReturnsOk_WhenExists()
        {
            // Arrange
            var url = $"/sites/{TestData.SiteId}/siteprofiles";

            // Act
            var response = await _httpClient.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<List<SiteProfileDTO>>();
            Assert.NotNull(result);
            Assert.Contains(result, sp => sp.SiteId == TestData.SiteId);
        }

        [Fact]
        public async Task GetByProfileId_ReturnsNotFound_WhenInvalidId()
        {
            var invalidId = Guid.NewGuid();

            var response = await _httpClient.GetAsync($"/profiles/{invalidId}/siteprofiles");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetBySiteId_ReturnsNotFound_WhenInvalidId()
        {
            var invalidId = Guid.NewGuid();

            var response = await _httpClient.GetAsync($"/sites/{invalidId}/siteprofiles");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
