using buzzerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebSockets.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace buzzerApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        //[HttpPost]
        //public ActionResult Login([FromBody] User user)
        //{
        //    var result = user.Email == "croquelois.adrien@gmail.com" && user.Password == "admin";
        //    if (result)
        //    {
        //        return new OkObjectResult(GenerateJwtToken(user.Email));
        //    }

        //    return NotFound();
        //}

    //    private string GenerateJwtToken(string email)
    //    {
    //        var claims = new List<Claim>
    //{
    //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    //    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
    //    new Claim(ClaimTypes.Email, email)
    //};

    //        //var keyByteArray = Encoding.ASCII.GetBytes(Constants.SecretKey);
    //        //var signinKey = new SymmetricSecurityKey(keyByteArray);

    //        var expires = DateTime.Now.AddDays(1);

    //        var token = new JwtSecurityToken(
    //            claims: claims,
    //            expires: expires,
    //            signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
    //        );

    //        return new JwtSecurityTokenHandler().WriteToken(token);
    //    }
    }

    
}
