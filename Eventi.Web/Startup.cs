using System;
using Eventi.Data.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReflectionIT.Mvc.Paging;
using Refit;
using Eventi.Sdk;
using AutoMapper;

namespace Eventi.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]     //?
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<MojContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("lokalni1"))); // za konstruktor

            services.AddAutoMapper(typeof(Startup));

            services.AddPaging(options =>
            {
                options.ViewName = "Bootstrap4";
                options.HtmlIndicatorDown = " <span>&darr;</span>";
                options.HtmlIndicatorUp = " <span>&uarr;</span>";
            });
            services.AddControllersWithViews();
            services.AddSignalR();

            services.AddMvc();
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddRefitClient<IAuthApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration.GetSection("EventiApi").Value));

            services.AddRefitClient<IEventiApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration.GetSection("EventiApi").Value));
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {   
                endpoints.MapControllerRoute(
                    name: "areaRoute",
                    pattern: "{area:exists}/{controller}/{action}" //,
                    /*defaults: new { action = "Index" }*/);   // moze i bez ovoga

                endpoints.MapControllerRoute(   
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
