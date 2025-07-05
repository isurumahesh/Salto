using CloudWorks.Application.DTOs.Pagination;
using CloudWorks.Application.DTOs.Sites;
using CloudWorks.IntegrationTests.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CloudWorks.IntegrationTests.Controllers
{
    public class SitesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private HttpClient _httpClient;

        public SitesControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;

            _httpClient = _factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        }

        [Fact]
        public async Task Get_ReturnsOkAndPagedResult_WhenValidInput()
        {
            // Arrange
            string url = "/sites?PageNumber=1&PageSize=10";

            // Act
            var response = await _httpClient.GetAsync(url);

            // Assert status code
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Assert content
            var pagedResult = await response.Content.ReadFromJsonAsync<PagedResult<SiteDTO>>();
            Assert.NotNull(pagedResult);
            Assert.True(pagedResult.PageNumber == 1);
            Assert.True(pagedResult.PageSize == 10);
            Assert.NotNull(pagedResult.Items);
        }

        [Fact]
        public async Task GetSiteById_ReturnsOk_WhenSiteExists()
        {
            // Arrange
            var siteId = TestData.SiteId; // seeded ID
            string url = $"/sites/{siteId}";

            // Act
            var response = await _httpClient.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var site = await response.Content.ReadFromJsonAsync<SiteDTO>();
            Assert.NotNull(site);
            Assert.Equal(siteId, site.Id);
        }

        [Fact]
        public async Task GetSiteById_ReturnsNotFound_WhenSiteDoesNotExist()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            string url = $"/sites/{invalidId}";

            // Act
            var response = await _httpClient.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_ReturnsCreated_WhenValidInput()
        {
            // Arrange
            var newSite = new AddSiteDTO
            {
                Name = "Integration Test Site",
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/sites", newSite);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdSite = await response.Content.ReadFromJsonAsync<SiteDTO>();
            Assert.NotNull(createdSite);
            Assert.Equal(newSite.Name, createdSite.Name);
        }

        [Fact]
        public async Task Put_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var updateDto = new UpdateSiteDTO
            {
                Id = TestData.SiteId,
                Name = "Updated Name",
            };

            // Act
            var response = await _httpClient.PutAsJsonAsync($"/sites/{updateDto.Id}", updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSiteDeleted()
        {
            // Arrange
            var siteId = TestData.SiteId;

            // Act
            var response = await _httpClient.DeleteAsync($"/sites/{siteId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task GetSites_ShouldReturnFirstPage_WithinReasonableTime()
        {
            // Arrange
            var stopwatch = new Stopwatch();
            string url = "/sites?PageNumber=1&PageSize=100";

            // Act
            stopwatch.Start();
            var response = await _httpClient.GetAsync(url);
            stopwatch.Stop();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var elapsed = stopwatch.ElapsedMilliseconds;
            Assert.True(elapsed < 750, $"Response took too long: {elapsed}ms");

            var result = await response.Content.ReadFromJsonAsync<PagedResult<SiteDTO>>();
            Assert.NotNull(result);
            Assert.Equal(100, result.Items.Count());
        }

        [Fact]
        public async Task GetSites_ReturnsFaster_OnSecondRequest_DueToCaching()
        {
            // Arrange
            string url = "/sites?PageNumber=1&PageSize=100";

            var stopwatch1 = new Stopwatch();
            var stopwatch2 = new Stopwatch();

            // Act
            stopwatch1.Start();
            var response1 = await _httpClient.GetAsync(url);
            stopwatch1.Stop();

            await Task.Delay(100);

            // Act
            stopwatch2.Start();
            var response2 = await _httpClient.GetAsync(url);
            stopwatch2.Stop();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response1.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response2.StatusCode);

            var time1 = stopwatch1.ElapsedMilliseconds;
            var time2 = stopwatch2.ElapsedMilliseconds;

            Assert.True(time2 < time1, $"Expected second call ({time2}ms) to be faster than first ({time1}ms)");
        }

        [Fact]
        public async Task Post_ReturnsUnauthorized_WhenNoAuthHeader()
        {
            _httpClient = _factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Invalid");

            var site = new AddSiteDTO { Name = "Unauthorized Site" };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/sites", site);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}