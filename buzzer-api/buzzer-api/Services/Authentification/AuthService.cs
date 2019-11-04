using buzzerApi.Models;
using buzzerApi.Options;
using buzzerApi.Services.Abstraction;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace buzzerApi.Services.Authentification
{
    public class AuthService : IAuthService
    {
        public AuthService(AuthOptions authOption)
        {
            _authOptions = authOption;
        }

        private AuthOptions _authOptions;

        /// <summary>
        /// Generates token by given user
        /// Ecrypt the token and return it
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GenerateToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim("app", "buzzer")
                }),
                Expires = DateTime.UtcNow.AddSeconds(_authOptions.ExpireMinutes),
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), _authOptions.SecurityAlgorithm)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
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
            byte[] symmetricKey = Convert.FromBase64String(_authOptions.SecretKey);
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
