using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buzzerApi.Enum;
using buzzerApi.Models;
using buzzerApi.Options;
using buzzerApi.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace buzzerApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {

        public QuestionController(IQuestionService questionService, IOptions<UploadOptions> uploadOptions, IUploadService uploadService)
        {
            _questionService = questionService;
            _uploadOptions = uploadOptions;
            _uploadService = uploadService;
        }

        private readonly IQuestionService _questionService;
        private readonly IOptions<UploadOptions> _uploadOptions;
        private readonly IUploadService _uploadService;

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
        public IActionResult PostQuestionTexte([FromBody] Question question)

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


        // POST api/values/postquestionmedia
        [HttpPost("{media}"), Authorize]
        public async Task<IActionResult> PostQuestionMedia(IFormCollection request, MediaType media)

        {
            try
            {
                if(!request.ContainsKey("question"))
                {
                    return BadRequest("Aucune question renseigné");
                }
                if (request.Files.Count == 0)
                {
                    return BadRequest("Aucun fichier envoyé");
                }
                var files = request.Files;
                var pathFiles = await _uploadService.UploadMedia(_uploadOptions, media, files);
                var key = request.Keys.FirstOrDefault(x => x == "question");
                Question question = JsonConvert.DeserializeObject<Question>(request[key]);
                ICollection<Propositions> propositions = new List<Propositions>();
                foreach (var path in pathFiles)
                {
                    var proposition = new Propositions();
                    proposition.proposition = path;
                    propositions.Add(proposition);
                }
                question.Propositions = propositions;
                var newQuestion = _questionService.CreateQuestion(question);
                return CreatedAtAction("GetQuestion", new { id = newQuestion.Id }, newQuestion);
            }
            catch (Exception ex)
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
