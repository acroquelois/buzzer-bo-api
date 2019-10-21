using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buzzerApi.Models;
using buzzerApi.Repository;
using buzzerApi.Repository.Abstraction;
using buzzerApi.Services;
using buzzerApi.Services.Abstraction;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace buzzer_api
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

            services
                .AddScoped<IQuestionTexteRepository, QuestionTexteRepository>()
                .AddScoped<IQuestionService, QuestionService>()
                .AddDbContext<BuzzerApiContext>(builder => builder.UseSqlServer(Configuration.GetConnectionString("BuzzerApiContext")));

            services.AddCors(actions =>

            {

                var corsPolicy = new CorsPolicy();

                corsPolicy.Headers.Add("*");

                corsPolicy.Methods.Add("*");

                corsPolicy.Origins.Add("*");

                actions.AddPolicy("policy", corsPolicy);

            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHttpsRedirection();
            }


            app
                .UseCors("policy")
                .UseMvc();

            // UseCors before Mvc

        }
    }
}
