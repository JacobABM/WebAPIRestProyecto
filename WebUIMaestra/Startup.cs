using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIMaestra
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(Options =>
                {
                    Options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                    Options.SlidingExpiration = true;
                    Options.LoginPath = "/Home/Login";
                    Options.LogoutPath = "/Admin/Home/Logout";
                    Options.AccessDeniedPath = "/Home/AccesoDenegado";
                    Options.Cookie.Name = "SesionNota";
                }
                );
            services.AddHttpClient("Evaluacion",options=>
            {
                options.BaseAddress = new Uri("https://192.168.1.69:45455/");
            }
            );
            services.AddMvc();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseFileServer();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute("areas", "{area:exists}/{Controller:Home}/{action:Index}/{Id?}");
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
