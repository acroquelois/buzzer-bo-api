using buzzerApi.Dto;
using buzzerApi.Enum;
using buzzerApi.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

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

        [HttpGet,Authorize]
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
