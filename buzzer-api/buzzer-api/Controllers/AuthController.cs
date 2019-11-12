using buzzerApi.Dto;
using buzzerApi.Enum;
using buzzerApi.Models;
using buzzerApi.Options;
using buzzerApi.Services.Abstraction;
using buzzerApi.Services.Authentification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebSockets.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace buzzerApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public ActionResult Login(
            [FromBody] UserAuth user,
            [FromServices] IAuthService authService
            )
        {
            try
            {
                var (authError,token) = authService.LoginAsync(new Models.User { Email = user.Email, Password = user.Password });
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

        [HttpGet]
        public ActionResult GetClaim()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    return Ok(claims);
                }
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, new { Message = "Server Error", Trace = e.StackTrace });
            }
        }
    }

    
}
