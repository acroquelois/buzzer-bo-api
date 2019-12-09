using buzzerApi.Dto;
using buzzerApi.Enum;
using buzzerApi.Options;
using buzzerApi.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace buzzerApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// User authentification.
        /// </summary>
        /// <param name="user">User model with username and password</param>
        /// <returns>An access token</returns>
        /// <response code="200">Returns the access token</response>
        /// <response code="401">Authentification failed</response>
        /// <response code="500">Server Error</response>
        [HttpPost,AllowAnonymous]
        public async Task<ActionResult> Login(
            [FromBody] UserAuth user,
            [FromServices] IAuthService authService
            )
        {
            try
            {
                var (authError,token) = await authService.LoginAsync(new Models.User { Email = user.Email, Password = user.Password });
                if (authError == AuthErrors.EmptyUsername)
                {
                    return Unauthorized(new { Message = "Utilisateur ou mot de passe incorrect." });
                }

                if (authError == AuthErrors.Forbidden)
                {
                    return Unauthorized(new { Message = "Utilisateur ou mot de passe incorrect." });
                }

                return Ok(token);
            }
            catch(Exception e)
            {
                return StatusCode(500, new { Message = "Server Error", Trace = e.StackTrace });
            }
        }
    }

    
}
