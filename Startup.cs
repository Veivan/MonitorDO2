using MonitorDO2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace MonitorDO2
{
    public class Startup
    {
        private static readonly string lc =
          "License Key=UdKWmLNCOGgy9gkeFWpZ0olVcfBkYIs4QQLxtS/9xaAgzJAmaLGASLr/laYll6NnBhW/P5c7ThG5CPmD/vgVajPO0ekFT8QH5dMLnw7wYSS5YK6hXbleGprFdA4NqO+OxdHvXrXp4RvxCqQtO1B/eoupKg4q78tTA+oqxNu+9geAlrY7oqXpr7S16GQClQOryptsZNGWvHgJricFrB6dwZlTFP8Mw9YjPQV2X1/46LBvHRJgzd/pQG40JPUKzeyJNuNzwn5dxL/qtJCaF1VqH9B7zxOAAajUcezX3OZpxfU=";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var ChaosConnectionDB = configuration.GetConnectionString("OracleConnection");

            ChaosConnectionDB += " " + lc; //не храним lic в конфиге
            services.AddTransient<IDoRepository, DoRepository>(provider => new DoRepository(ChaosConnectionDB));
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
