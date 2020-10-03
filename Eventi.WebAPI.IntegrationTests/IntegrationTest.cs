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
using Eventi.Core.Helpers;

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
                        // Remove the app's ApplicationDbContext registration.
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                                typeof(DbContextOptions<EventiContext>));

                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }

                        // Add ApplicationDbContext using an in-memory database for testing.
                        services.AddDbContext<EventiContext>(options =>
                        {
                            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                        });

                        // Build the service provider.
                        var sp = services.BuildServiceProvider();

                        using (var scope = sp.CreateScope())
                        {
                            var scopedServices = scope.ServiceProvider;

                            var db = scopedServices.GetRequiredService<EventiContext>();

                            Seed(db);
                        }
                    });

                });

            _serviceProvider = appFactory.Services;
            _httpClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }


        private async Task<string> GetJwtAsync()
        {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Auth.Login, new LoginRequest
            {
                Username = "admin",
                Password = "password1"
            });

            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }

        private void Seed(EventiContext context)
        {
            context.Database.EnsureDeleted();

            var list = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                list.Add(HashHelper.GenerateSalt());
            }

            context.Accounts.AddRange
                (
                    new List<Account>()
                    {
                        new Account
                        {
                            AccountCategory = Common.AccountCategory.Administrator,
                            Email = "admin@test.com",
                            Username = "admin",
                            PasswordSalt = list[0],
                            PasswordHash = HashHelper.GenerateHash(list[0], "password1")
                        },
                        new Account
                        {
                            AccountCategory = Common.AccountCategory.Organizer,
                            Email = "organizer@test.com",
                            Username = "organizer",
                            PasswordSalt = list[1],
                            PasswordHash = HashHelper.GenerateHash(list[1], "password1")
                        },
                        new Account
                        {
                            AccountCategory = Common.AccountCategory.Client,
                            Email = "client@test.com",
                            Username = "client",
                            PasswordSalt = list[2],
                            PasswordHash = HashHelper.GenerateHash(list[2], "password1")
                        }
                    }
                );

            context.Countries.AddRange
                (
                    new List<Country>()
                    {
                        new Country { Name = "Bosnia and Herzegovina"},
                        new Country { Name = "Croatia"},
                        new Country { Name = "Serbia"}
                    }
                );


            context.Cities.AddRange
                (
                    new List<City>()
                    {
                        new City { CountryID = 1, Name = "Sarajevo"},
                        new City { CountryID = 1, Name = "Mostar"},
                        new City { CountryID = 1, Name = "Zenica"},
                        new City { CountryID = 1, Name = "Tuzla"},
                        new City { CountryID = 1, Name = "Banja Luka"},
                        new City { CountryID = 1, Name = "Bihać"},
                        new City { CountryID = 2, Name = "Zagreb"},
                        new City { CountryID = 2, Name = "Split"},
                        new City { CountryID = 3, Name = "Beograd"},
                        new City { CountryID = 3, Name = "Novi Sad"},
                    }
                );

            context.SaveChanges();
        }

        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<EventiContext>();
            context.Database.EnsureDeleted();
        }
    }
}
