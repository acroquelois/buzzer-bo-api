using buzzerApi.Models;
using buzzerApi.Options;
using buzzerApi.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading.Tasks;

namespace buzzerApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {



        public UserController(ILogger<UserController> logger, IOptions<LogEventOptions> logOptions, IUserService userService)
        {
            _logger = logger;
            _logOptions = logOptions;
            _userService = userService;
        }

        private readonly ILogger<UserController> _logger;
        private readonly IOptions<LogEventOptions> _logOptions;
        private readonly IUserService _userService;

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
            [FromBody] User user
            )
        {
            try
            {
                User newUser = await _userService.CreateUserAsync(user);
                _logger.LogInformation(_logOptions.Value.CreateItem,"User : A new user was create");
                return CreatedAtAction("GetUser", new { id = newUser.Id}, newUser);
            }
            catch (Exception e)
            {
                _logger.LogError(_logOptions.Value.CreateItem,"User : Error at user creation");
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
            string mail
            )
        {
            try
            {
                var ret = await _userService.GetUserAsync(mail);
                if(ret == null)
                {
                    _logger.LogWarning(_logOptions.Value.GetItem, "The user {mail} doesn't exist", mail);
                    return NotFound("This email doesn't exist");
                }
                _logger.LogInformation(_logOptions.Value.GetItem, "The user {user} was get by his mail", mail);
                return Ok(ret);
            }
            catch (Exception e)
            {
                _logger.LogError(_logOptions.Value.GetItem, "Server error at get user by mail");
                return BadRequest(e);
            }
        }

        [HttpGet]
        public ActionResult testenv()
        {
           try
           {
                var enumerator = Environment.GetEnvironmentVariables().GetEnumerator();
                var test = "";
                while (enumerator.MoveNext())
                {
                    test = String.Concat(test , ($"{enumerator.Key,5}:{enumerator.Value,100} ---------"));
                }
                return Ok(test);
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