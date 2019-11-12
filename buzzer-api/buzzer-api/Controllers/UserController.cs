using buzzerApi.Models;
using buzzerApi.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateUser(
            [FromBody] User user,
            [FromServices] IUserService userService
            )
        {
            try
            {
                var ret = await userService.CreateUserAsync(user);
                return Ok(ret);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public ActionResult GetUserByMail(
            string mail,
            [FromServices] IUserService userService
            )
        {
            try
            {
                var ret = userService.GetUserAsync(mail);
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }


}