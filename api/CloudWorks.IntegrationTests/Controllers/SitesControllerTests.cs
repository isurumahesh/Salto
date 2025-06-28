using CloudWorks.Application.DTOs.Pagination;
using CloudWorks.Application.DTOs.Sites;
using CloudWorks.Data.Database;
using CloudWorks.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        public async Task Get_ReturnsOkAndPagedResult()
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
    }
}
