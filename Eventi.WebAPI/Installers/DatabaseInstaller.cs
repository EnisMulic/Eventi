using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Eventi.Database;
using Eventi.Services;

namespace Eventi.WebAPI.Installers
{
    public class DatabaseInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<EventiContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Eventi")));

            // Auth
            services.AddScoped<IAuthService, AuthService>();

            // Event
            services.AddScoped<ICRUDService<EventResponse, EventSearchRequest, EventInsertRequest, EventUpdateRequest>, EventService>();

            // User
            //services.AddScoped<ICRUDService<UserResponse, UserSearchRequest, UserInsertRequest, UserUpdateRequest>, UserService>();

            // City
            services.AddScoped<ICityService, CityService>();

            // Country
            services.AddScoped<ICountryService, CountryService>();

            // Venue
            services.AddScoped<IVenueService, VenueService>();

            // Performer
            services.AddScoped<ICRUDService<PerformerResponse, PerformerSearchRequest, PerformerUpsertRequest, PerformerUpsertRequest>, PerformerService>();

            // Sponsor
            services.AddScoped<ICRUDService<SponsorResponse, SponsorSearchRequest, SponsorUpsertRequest, SponsorUpsertRequest>, SponsorService>();
        }
    }
}