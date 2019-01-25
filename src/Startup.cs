using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ticketing.Web.Hubs;
using Ticketing.Web.Clients;

namespace Ticketing.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.Configure<AppConfiguration>(Configuration.GetSection("AppConfiguration"));
            services.Configure<AppConfiguration>(Configuration);

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
            });

            bool localSignalR = String.IsNullOrEmpty(Configuration.GetSection("AppConfiguration").GetValue<string>("AzureSignalRConnectionString"));

            if(localSignalR)
            {
                Console.WriteLine("Local SignalR Configure Service");
                services.AddSignalR()
                        .AddMessagePackProtocol();
            }
            else
            {
                Console.WriteLine("Azure SignalR Configure Service");
                services.AddSignalR()
                    .AddAzureSignalR(Configuration.GetSection("AppConfiguration").GetValue<string>("AzureSignalRConnectionString"))
                    .AddMessagePackProtocol();
            }

            services.AddSingleton<WorkerClient>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            bool localSignalR = String.IsNullOrEmpty(Configuration.GetSection("AppConfiguration").GetValue<string>("AzureSignalRConnectionString"));

            if(localSignalR)
            {
                Console.WriteLine("Local SignalR Configure");
                app.UseSignalR(routes =>
                {
                    routes.MapHub<WorkerHub>("/workers");
                });
            }
            else
            {
                Console.WriteLine("Azure SignalR Configure");
                app.UseAzureSignalR(routes =>
                {
                    routes.MapHub<WorkerHub>("/workers");
                });
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
