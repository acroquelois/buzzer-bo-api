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
        public async Task<ActionResult<IEnumerable<QuestionTexte>>> Get()
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
        [HttpPost]
        public IActionResult PostQuestionTexte([FromBody] QuestionTexte question)
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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
