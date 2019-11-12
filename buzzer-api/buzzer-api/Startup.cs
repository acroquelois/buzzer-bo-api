using buzzerApi.Models;
using buzzerApi.Options;
using buzzerApi.Repository;
using buzzerApi.Repository.Abstraction;
using buzzerApi.Services;
using buzzerApi.Services.Abstraction;
using buzzerApi.Services.Authentification;
using buzzerApi.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
                .AddScoped<IQuestionRepository, QuestionRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IQuestionService, QuestionService>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IPasswordHasher<User>, PasswordHasher<User>>()

                .AddScoped<AuthOptions>()
                .AddDbContext<BuzzerApiContext>(
                options => options.UseMySQL(Configuration.GetConnectionString("BuzzerApiContext")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["http://localhost/5000"],
                        ValidAudience = Configuration["http://localhost/5000"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Tw9dsdregfddshfusd"))
                    };
                });
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
            }


            app
                .UseCors("policy")
                .UseMvc()
                .UseAuthentication();

            // UseCors before Mvc

        }
    }
}
