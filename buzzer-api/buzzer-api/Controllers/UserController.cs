using buzzerApi.Models;
using buzzerApi.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

namespace buzzerApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("{id}", Name="GetUser")]
        public string GetUser(Guid id)
        {
            return "value";
        }

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> CreateUser(
            [FromBody] User user,
            [FromServices] IUserService userService
            )
        {
            try
            {
                User newUser = await userService.CreateUserAsync(user);
                return CreatedAtAction("GetUser", new { id = newUser.Id}, newUser);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet, Authorize]
        public async Task<ActionResult> GetUserByMail(
            string mail,
            [FromServices] IUserService userService
            )
        {
            try
            {
                var ret = await userService.GetUserAsync(mail);
                if(ret == null)
                {
                    return NotFound("This email doesn't exist");
                }
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //[HttpGet, Authorize]
        //public ActionResult SetSession()
        //{
        //    try
        //    {
        //        var context = HttpContext.Session;
        //        byte[] ret = Encoding.ASCII.GetBytes("true");
        //        context.Set("test",ret);
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e);
        //    }
        //}

        //[HttpGet, Authorize]
        //public ActionResult GetSession()
        //{
        //    try
        //    {
        //        var context = HttpContext.Session;
        //        var ret = context.Get("test");
        //        return Ok(ret);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e);
        //    }
        //}

    }


}