﻿using buzzerApi.Dto;
using buzzerApi.Enum;
using buzzerApi.Models;
using buzzerApi.Options;
using buzzerApi.Services.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace buzzerApi.Services.Authentification
{
    public class AuthService : IAuthService
    {
        public AuthService(AuthOptions authOption, IPasswordHasher<Models.User> passwordHasher, IUserService userService)
        {
            _authOptions = authOption;
            _passwordHasher = passwordHasher;
            _userService = userService;
        }

        private readonly AuthOptions _authOptions;
        private readonly IPasswordHasher<Models.User> _passwordHasher;
        private readonly IUserService _userService;

        public async Task<(AuthErrors, UserToken)> LoginAsync(Models.User userAuth)
        {
            if (userAuth == null)
            {
                return (AuthErrors.EmptyUsername, null);
            }
            if (string.IsNullOrEmpty(userAuth.Email))
            {
                return (AuthErrors.EmptyUsername, null);
            }
            var user = await _userService.GetUserAsync(userAuth.Email);
            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.Password, userAuth.Password.Trim());

            if (result == PasswordVerificationResult.Failed)
            {
                return (AuthErrors.Forbidden, null);
            }

            UserToken accessToken = this.GenerateToken(user);

            return (AuthErrors.None, accessToken);
        }


        /// <summary>
        /// Generates token by given user
        /// Ecrypt the token and return it
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserToken GenerateToken(Models.User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var keyByteArray = Encoding.ASCII.GetBytes(_authOptions.SecretKey);
            var signinKey = new SymmetricSecurityKey(keyByteArray);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Sid, user.Email.ToString()),
                    new Claim("app", "buzzer")
                }),
                Expires = DateTime.UtcNow.AddSeconds(_authOptions.ExpireMinutes),
                SigningCredentials = new SigningCredentials(signinKey, _authOptions.SecurityAlgorithm)
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