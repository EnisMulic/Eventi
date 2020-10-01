using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Eventi.Database;
using Eventi.Contracts.V1.Responses;
using Eventi.Contracts.V1;
using Eventi.Contracts.V1.Requests;
using Eventi.Domain;
using System.Collections.Generic;

namespace Eventi.WebAPI.IntegrationTests
{
    public class IntegrationTest : IDisposable
    {
        protected readonly HttpClient _httpClient;
        private readonly IServiceProvider _serviceProvider;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                                typeof(DbContextOptions<EventiContext>));

                        services.Remove(descriptor);

                        services.AddDbContext<EventiContext>(options =>
                        {
                            options.UseInMemoryDatabase("EventiTestDb");
                        });

                        var sp = services.BuildServiceProvider();
                    });

                });

            _serviceProvider = appFactory.Services;
            using var serviceScope = _serviceProvider.CreateScope();

            var context = serviceScope.ServiceProvider.GetService<EventiContext>();
            Seed(context);

            _httpClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }


        private async Task<string> GetJwtAsync()
        {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Auth.RegisterClient, new ClientRegistrationRequest
            {
                Username = "Integrator",
                Email = "test2@integration.com",
                Password = "SomePass1234!"
            });

            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }

        private void Seed(EventiContext context)
        {
            context.Countries.AddRange
                (
                    new List<Country>()
                    {
                        new Country { Name = "Bosnia and Herzegovina"},
                        new Country { Name = "Croatia"},
                        new Country { Name = "Serbia"}
                    }
                );
            context.SaveChanges();
        }

        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<EventiContext>();
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
