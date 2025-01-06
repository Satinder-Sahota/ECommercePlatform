using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePlatform.Tests.IntegrationTests
{
    public class CartIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public CartIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient(); 
        }
        [Fact]
        public async Task AddToCart_ReturnsSuccess()
        {
            //Arrange
            var productId = 1;
            //Act
            var response = await _client.PostAsync($"/Cart/AddToCart?productId={productId}", null);
            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
