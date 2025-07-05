using CloudWorks.Application.DTOs.Profiles;
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
    public class ProfilesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public ProfilesControllerTests(CustomWebApplicationFactory<Program> factory)
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
        public async Task Create_ReturnsCreated_WhenValidInput()
        {
            var dto = new AddProfileDTO
            {
                Email = "newuser@example.com",
                IdentityId = Guid.NewGuid()
            };

            var response = await _httpClient.PostAsJsonAsync("/profiles", dto);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var created = await response.Content.ReadFromJsonAsync<ProfileDTO>();
            Assert.NotNull(created);
            Assert.Equal(dto.Email, created.Email);
            Assert.Equal(dto.IdentityId, created.IdentityId);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_WhenInvalidEmail()
        {
            var dto = new AddProfileDTO
            {
                Email = "invalid-email",
                IdentityId = Guid.NewGuid()
            };

            var response = await _httpClient.PostAsJsonAsync("/profiles", dto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_ReturnsProfiles()
        {
            var response = await _httpClient.GetAsync("/profiles");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<List<ProfileDTO>>();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetById_ReturnsProfile_WhenExists()
        {
            var response = await _httpClient.GetAsync($"/profiles/{TestData.ProfileId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var profile = await response.Content.ReadFromJsonAsync<ProfileDTO>();
            Assert.NotNull(profile);
            Assert.Equal(TestData.ProfileId, profile.Id);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WithUnknownId()
        {
            var unknownId = Guid.NewGuid();

            var response = await _httpClient.GetAsync($"/profiles/{unknownId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
