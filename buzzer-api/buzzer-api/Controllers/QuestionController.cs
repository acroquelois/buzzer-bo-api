using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using buzzerApi.Models;
using buzzerApi.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            try
            {
                var questions = await _questionService.GetListAllQuestion();
                if (questions == null)
                {
                    return NotFound("There is no question");
                }
                return Ok(questions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> GetQuestion(int id)
        {
            return "value";
        }

        // POST api/values/postquestion
        [HttpPost, Authorize]
        public IActionResult PostQuestion([FromBody] Question question)

        {
            try
            {
                var newQuestion =  _questionService.CreateQuestion(question);
                return CreatedAtAction("GetQuestion", new { id = newQuestion.Id}, newQuestion);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/values/delete
        [HttpDelete, Authorize]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            try
            {
                var question = await _questionService.DeleteQuestion(id);
                if (!question)
                {
                    return NotFound("The question doesn't not exist");
                }
                return Ok("The question was successfully deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
