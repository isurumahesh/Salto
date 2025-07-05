using CloudWorks.Api.Endpoints.Requests;
using CloudWorks.Application.DTOs.AccessPoints;
using CloudWorks.Application.DTOs.Pagination;
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
    public class AccessPointsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public AccessPointsControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        }

        [Fact]
        public async Task Get_ReturnsPagedAccessPoints()
        {
            var response = await _httpClient.GetAsync($"/sites/{TestData.SiteId}/accessPoints?PageNumber=1&PageSize=10");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<PagedResult<AccessPointDTO>>();
            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
        }

        [Fact]
        public async Task GetById_ReturnsAccessPoint_WhenExists()
        {
            var response = await _httpClient.GetAsync($"/sites/{TestData.SiteId}/accessPoints/{TestData.AccessPointId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<AccessPointDTO>();
            Assert.NotNull(result);
            Assert.Equal(TestData.AccessPointId, result.Id);
        }

        [Fact]
        public async Task OpenAccessPoint_ReturnsSuccess()
        {
            var request = new OpenAccessPointDTO { ProfileId = TestData.ProfileId };

            var response = await _httpClient.PostAsJsonAsync(
                $"/sites/{TestData.SiteId}/accessPoints/{TestData.AccessPointId}/open", request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task AddAccessPoint_ReturnsCreated_WhenValid()
        {
            var dto = new AddAccessPointDTO
            {
                Name = "Integration AP",
                SiteId = TestData.SiteId
            };

            var response = await _httpClient.PostAsJsonAsync($"/sites/{TestData.SiteId}/accessPoints", dto);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var created = await response.Content.ReadFromJsonAsync<AccessPointDTO>();
            Assert.NotNull(created);
            Assert.Equal(dto.Name, created.Name);
        }

        [Fact]
        public async Task AddAccessPoint_ReturnsBadRequest_WhenInvalid()
        {
            var dto = new AddAccessPointDTO
            {
                Name = "", // Invalid
                SiteId = Guid.Empty // Invalid
            };

            var response = await _httpClient.PostAsJsonAsync($"/sites/{TestData.SiteId}/accessPoints", dto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAccessPoint_ReturnsNoContent_WhenValid()
        {
            var dto = new UpdateAccessPointDTO
            {
                Id = TestData.AccessPointId,
                Name = "Updated AP",
                SiteId = TestData.SiteId
            };

            var response = await _httpClient.PutAsJsonAsync($"/sites/{TestData.SiteId}/accessPoints/{dto.Id}", dto);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAccessPoint_ReturnsBadRequest_WhenIdsMismatch()
        {
            var dto = new UpdateAccessPointDTO
            {
                Id = Guid.NewGuid(), // Mismatched ID
                Name = "Mismatch AP",
                SiteId = TestData.SiteId
            };

            var response = await _httpClient.PutAsJsonAsync($"/sites/{TestData.SiteId}/accessPoints/{TestData.AccessPointId}", dto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteAccessPoint_ReturnsNoContent_WhenExists()
        {
            var response = await _httpClient.DeleteAsync($"/sites/{TestData.SiteId}/accessPoints/{TestData.AccessPointId}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

    }
}
