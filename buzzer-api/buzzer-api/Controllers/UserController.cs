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

        /// <summary>
        /// Get a user by his id.
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns>The specified user</returns>
        /// <response code="200">Returns the user</response>
        [HttpGet("{id}", Name="GetUser"), Authorize]
        public string GetUser(Guid id)
        {
            return "value";
        }

        /// <summary>
        /// Return a random question.
        /// </summary>
        /// <param name="user">User model</param>
        /// <returns>A random question</returns>
        /// <response code="200">Returns the question</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Question not found</response>
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

        /// <summary>
        /// Get a user by his mail.
        /// </summary>
        /// <param name="mail">Mail of the user</param>
        /// <returns>The specified user</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Mail doesn't match with a user</response>
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