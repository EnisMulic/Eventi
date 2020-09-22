using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Eventi.Database;
using Eventi.Domain;
using Eventi.Services;

namespace Eventi.WebAPI.Installers
{
    public class DatabaseInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TemplateContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("TemplateAPI")));
            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TemplateContext>();


            // Auth
            services.AddScoped<IUserAccountService, UserAccountService>();

            // User
            services.AddScoped<ICRUDService<UserResponse, UserSearchRequest, UserInsertRequest, UserUpdateRequest>, UserService>();
        }
    }
}