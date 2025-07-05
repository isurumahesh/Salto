using CloudWorks.Data.Contracts.Entities;
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
    public class AccessEventsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private HttpClient _httpClient;

        public AccessEventsControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        }

        [Fact]
        public async Task GetAccessEventsForSite_ReturnsOk_WhenSiteExists()
        {
            // Arrange
            string url = $"/sites/{TestData.SiteId}/accessEvents";

            // Act
            var response = await _httpClient.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var accessEvents = await response.Content.ReadFromJsonAsync<List<AccessEvent>>();
            Assert.NotNull(accessEvents);
            Assert.True(accessEvents.Count >= 1);
        }

        [Fact]
        public async Task GetAccessEventsForSite_ReturnsUnauthorized_WhenNoAuthHeader()
        {
            // Arrange
            _httpClient = _factory.CreateClient(); // No auth header
            string url = $"/sites/{TestData.SiteId}/accessEvents";

            // Act
            var response = await _httpClient.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
