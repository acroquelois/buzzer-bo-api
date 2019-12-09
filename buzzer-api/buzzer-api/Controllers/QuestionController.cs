using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using buzzerApi.Models;
using buzzerApi.Services.Abstraction;
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

        // GET api/question/get
        [HttpGet]
        public ActionResult<IEnumerable<Question>> Get()
        {
            try
            {
                var question = _questionService.GetListAllQuestionTexte();
                return Ok(question);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Question> Get(Guid id)
        {
            try
            {
                var question = _questionService.GetListOneQuestionTexte(id);
                return Ok(question);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/values/postquestiontexte
        [HttpPost]
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
        [HttpDelete("{id}")]
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

        // GET api/question/random
        [HttpGet]
        public ActionResult<Question> Random()
        {
            var question = _questionService.GetListRandomQuestionTexte();
            return Ok(question);
        }
    }
}
