using buzzerApi.Models;
using buzzerApi.Options;
using buzzerApi.Repository;
using buzzerApi.Repository.Abstraction;
using buzzerApi.Services;
using buzzerApi.Services.Abstraction;
using buzzerApi.Services.Authentification;
using buzzerApi.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace buzzerApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IQuestionRepository, QuestionRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IQuestionService, QuestionService>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        }

        public static IServiceCollection AddCustomAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(x =>
                    {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(x =>
                {
                    var keyByteArray = Encoding.ASCII.GetBytes(configuration?.GetSection("Auth")?["Key"]);
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(keyByteArray),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            return services;
        }

        /// <summary>
        /// Registers <see cref="IOptions{TOptions}"/> and <typeparamref name="TOptions"/> to the services container.
        /// Also runs data annotation validation.
        /// </summary>
        /// <typeparam name="TOptions">The type of the options.</typeparam>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same services collection.</returns>
        public static IServiceCollection ConfigureAndValidateSingleton<TOptions>(
            this IServiceCollection services,
            IConfiguration configuration)
            where TOptions : class, new()
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services
                .AddOptions<TOptions>()
                .Bind(configuration)
                .ValidateDataAnnotations();
            return services.AddSingleton(x => x.GetRequiredService<IOptions<TOptions>>().Value);
        }

        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .ConfigureAndValidateSingleton<AuthOptions>(configuration.GetSection(nameof(ApplicationOptions.Auth)))
                .ConfigureAndValidateSingleton<UploadOptions>(configuration.GetSection(nameof(ApplicationOptions.Upload)));
            return services;
        }
    }
}
