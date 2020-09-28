using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eventi.WebAPI.Installers
{
    public class AutoMapperInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(Core.Mappings.VenueProfile));
            services.AddAutoMapper(typeof(Core.Mappings.CityProfile));
            services.AddAutoMapper(typeof(Core.Mappings.CountryProfile));
            services.AddAutoMapper(typeof(Core.Mappings.PerformerProfile));
            services.AddAutoMapper(typeof(Core.Mappings.TicketProfile));
            services.AddAutoMapper(typeof(Core.Mappings.OrganizerProfile));
            services.AddAutoMapper(typeof(Core.Mappings.SponsorProfile));
            services.AddAutoMapper(typeof(Core.Mappings.EventProfile));
            services.AddAutoMapper(typeof(Core.Mappings.AccountProfile));
            services.AddAutoMapper(typeof(Core.Mappings.PersonProfile));
            services.AddAutoMapper(typeof(Core.Mappings.AdministratorProfile));
            services.AddAutoMapper(typeof(Core.Mappings.ClientProfile));
        }
    }
}