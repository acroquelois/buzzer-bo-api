using buzzerApi.Dto;
using buzzerApi.Enum;
using buzzerApi.Options;
using buzzerApi.Services.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace buzzerApi.Services.Authentification
{
    public class AuthService : IAuthService
    {
        public AuthService(IPasswordHasher<Models.User> passwordHasher, IUserService userService, IOptions<AuthOptions> authOptions, IConfiguration configuration)
        {
            _passwordHasher = passwordHasher;
            _userService = userService;
            _configuration = configuration;
            _authOptions = authOptions;
        }

        private readonly IPasswordHasher<Models.User> _passwordHasher;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IOptions<AuthOptions> _authOptions;

        public async Task<(EnumAuthErrors, UserToken)> LoginAsync(Models.User userAuth)
        {
            if (userAuth == null)
            {
                return (EnumAuthErrors.EmptyUsername, null);
            }
            if (string.IsNullOrEmpty(userAuth.Email))
            {
                return (EnumAuthErrors.EmptyUsername, null);
            }
            try
            {
                var user = await _userService.GetUserAsync(userAuth.Email);
                PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.Password, userAuth.Password.Trim());

                if (result == PasswordVerificationResult.Failed)
                {
                    return (EnumAuthErrors.Forbidden, null);
                }

                UserToken accessToken = this.GenerateToken(user);

                return (EnumAuthErrors.None, accessToken);
            }
            catch
            {
                return (EnumAuthErrors.Forbidden, null);
            }
        }


        /// <summary>
        /// Generates token by given user
        /// Ecrypt the token and return it
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserToken GenerateToken(Models.User user)
        {
            var authOptions = _authOptions.Value;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var keyByteArray = Encoding.ASCII.GetBytes(authOptions.Key);
            var signinKey = new SymmetricSecurityKey(keyByteArray);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Sid, user.Email.ToString()),
                    new Claim("app", "buzzer")
                }),
                Expires = DateTime.UtcNow.AddSeconds(authOptions.ExpireMinutes),
                SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new UserToken()
            {
                Token = tokenHandler.WriteToken(token)
            };
        }

        /// <summary>
        /// Validates whether a given token is valid or not, and returns true in case the token is valid otherwise it will return false
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool IsTokenValid(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentException("Le token donné est null ou vide");

            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Takes the secret key and converts it to byte array and returns new SecurityKey
        /// </summary> 
        private SecurityKey GetSymmetricSecurityKey()
        {
            var authOptions = _authOptions.Value;
            byte[] symmetricKey = Convert.FromBase64String(authOptions.Key);
            return new SymmetricSecurityKey(symmetricKey);
        }

        /// <summary>
        /// Creates new instance of TokenValidationParameters
        /// </summary>
        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSymmetricSecurityKey()
            };
        }
    }
}
