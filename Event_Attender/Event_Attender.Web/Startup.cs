using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event_Attender.Data.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using ReflectionIT.Mvc.Paging;

namespace Event_Attender.Web
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
               options.UseSqlServer(Configuration.GetConnectionString("proba1"))); // za konstruktor

            services.AddPaging(options =>
            {
                options.ViewName = "Bootstrap4";
                options.HtmlIndicatorDown = " <span>&darr;</span>";
                options.HtmlIndicatorUp = " <span>&uarr;</span>";
            });
            services.AddControllersWithViews();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
