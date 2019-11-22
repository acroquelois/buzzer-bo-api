using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using buzzerApi.Models;
using buzzerApi.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace buzzerApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        private readonly IQuestionService _questionService;

        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Question>>> Get()
        {
            try
            {
                var question = await _questionService.GetListAllQuestionTexte();
                return Ok(question);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values/postquestiontexte
        [HttpPost, Authorize]
        public IActionResult PostQuestionTexte([FromBody] Question question)
        {
            try
            {
                _questionService.CreateQuestionTexte(question);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/values/delete
        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _questionService.DeleteQuestion(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
