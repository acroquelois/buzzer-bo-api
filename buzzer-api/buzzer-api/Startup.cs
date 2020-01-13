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
using System.IO;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;

namespace buzzer_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Startup()
        {
            _configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();
        }

        public IConfiguration _configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //string mysql = String.Concat("server=", Environment.GetEnvironmentVariable("DATABASE_URL"));
            //mysql = String.Concat(mysql, ";user=root;database=buzzer;password=admin");
            //"server=db;user=root;database=buzzer;password=admin"
            //_configuration.GetConnectionString("BuzzerApiContext")
            services
                .AddCustomServices()
                .AddCustomAuth(_configuration)
                .AddCustomOptions(_configuration)
                .AddDbContext<BuzzerApiContext>(
                options => options.UseMySQL("server=db;user=root;database=buzzer;password=admin"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Buzzer Api", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

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
                .UseMvc()
                .UseStaticFiles()
                .UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Buzzer API V1");
                    c.InjectStylesheet("/swagger-ui/theme-material.css");
                });

            UpdateDatabase(app);

            // UseCors before Mvc

        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<BuzzerApiContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
