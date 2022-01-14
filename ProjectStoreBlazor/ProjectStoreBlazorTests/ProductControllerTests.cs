using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProjectStoreBlazor.Server;
using ProjectStoreBlazor.Server.Entities;
using ProjectStoreBlazor.Shared.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProjectStoreBlazorTests
{
    public class ProductControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client;
        WebApplicationFactory<Startup> _factory;
        public ProductControllerTests(WebApplicationFactory<Startup> factory)
        {


            _factory = factory
                .WithWebHostBuilder(builder =>
                {

                    builder.ConfigureServices(services =>
                    {
                        var dbcontextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<StoreDbContext>));
                        services.Remove(dbcontextOptions);
                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                        services.AddMvc(option => option.Filters.Add(new FakeUserFilter()));
                        services.AddEntityFrameworkInMemoryDatabase().AddDbContext<StoreDbContext>(options => options.UseInMemoryDatabase("StoreDb"));
                    });
                });
            _client = _factory.CreateClient();
                
        }

        [Fact]
        public async Task CreateProduct_WithValidModel_ReturnOkStatus()
        {
            //arange
            var model = new ProductDto()
            {
                Name = "bacardi",
                Description = "w Klubie ze szklanki",
                IsAvailable = true,
                Price = 420,

            };
            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            //act
            var response = await _client.PostAsync("api/Product", httpContent);
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            

        }
        [Fact]
        public async Task DeleteProduct_ForNonExistingProduct_ReturnsNotFound()
        {   //arange
            var productId = 997;
            //act
            var response = await _client.DeleteAsync($"/api/Product/{productId}");
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
           
        }
        [Fact]
        public async Task Delete_ForValidUser_ReturnsNoContent() 
        {
            //arrange
           
            var product = new Product
            {
                CreatedByUserId = 1,
                Name = "Test"
            };
            //seed
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<StoreDbContext>();
            //act
            var response = await _client.DeleteAsync($"api/Product/{product.Id}");

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);


        }


        [Fact]
        public async void GetElementById_WithValidIdParameter_ReturnOkResult()
        {
            //arange
            var model = new ProductDto()
            {
                Name = "testproduct1",
                Description = "producttotest",
                IsAvailable = true,
                Price = 420,

            };
            //act
            var response = await _client.GetAsync($"/Product/Get/{1}");
            //seed
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<StoreDbContext>();

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}