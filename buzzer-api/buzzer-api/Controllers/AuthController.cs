using buzzerApi.Dto;
using buzzerApi.Enum;
using buzzerApi.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
            [FromServices] IAuthService authService,
            [FromServices] ILogger<AuthController> logger

            )
        {
            try
            {
                var (authError,token) = await authService.LoginAsync(new Models.User { Email = user.Email, Password = user.Password });
                if (authError == EnumAuthErrors.EmptyUsername)
                {
                    logger.LogWarning("Connexion attempt failed by empty user");
                    return Unauthorized(new { Message = "Utilisateur ou mot de passe incorrect." });
                }

                if (authError == EnumAuthErrors.Forbidden)
                {
                    logger.LogInformation("Connexion attempt failed by {User}", user.Email);
                    return Unauthorized(new { Message = "Utilisateur ou mot de passe incorrect." });
                }
                logger.LogInformation("Connexion by {User}", user.Email);
                return Ok(token);
            }
            catch(Exception e)
            {
                logger.LogWarning("Server error at user connexion");
                return StatusCode(500, new { Message = "Server Error", Trace = e.StackTrace });
            }
        }
    }

    
}
