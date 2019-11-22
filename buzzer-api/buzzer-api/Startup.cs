using buzzerApi.Extensions;
using buzzerApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace buzzer_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCustomServices()
                .AddCustomAuth(_configuration)
                .AddDbContext<BuzzerApiContext>(
                options => options.UseMySQL(_configuration.GetConnectionString("BuzzerApiContext")));

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
            });




            services.AddCors(actions =>

            {

                var corsPolicy = new CorsPolicy();

                corsPolicy.Headers.Add("*");

                corsPolicy.Methods.Add("*");

                corsPolicy.Origins.Add("*");

                actions.AddPolicy("policy", corsPolicy);

            });

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            app
                .UseCors("policy")
                .UseAuthentication()
                .UseSession() 
                .UseMvc();
                

            // UseCors before Mvc

        }
    }
}
