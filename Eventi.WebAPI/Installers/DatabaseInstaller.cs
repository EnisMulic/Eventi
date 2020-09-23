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
            //services.AddScoped<IUserAccountService, UserAccountService>();

            // User
            //services.AddScoped<ICRUDService<UserResponse, UserSearchRequest, UserInsertRequest, UserUpdateRequest>, UserService>();
        }
    }
}