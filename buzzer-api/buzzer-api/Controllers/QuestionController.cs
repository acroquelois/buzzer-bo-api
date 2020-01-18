using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buzzerApi.Dto;
using buzzerApi.Enum;
using buzzerApi.Models;
using buzzerApi.Options;
using buzzerApi.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace buzzerApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {

        public QuestionController(IQuestionService questionService, IOptions<UploadOptions> uploadOptions, IUploadService uploadService, ILogger<QuestionController> logger, IOptions<LogEventOptions> logEvent)
        {
            _questionService = questionService;
            _uploadOptions = uploadOptions;
            _uploadService = uploadService;
            _logger = logger;
            _logEvent = logEvent;

        }

        private readonly IQuestionService _questionService;
        private readonly IOptions<UploadOptions> _uploadOptions;
        private readonly IUploadService _uploadService;
        private readonly ILogger<QuestionController> _logger;
        private readonly IOptions<LogEventOptions> _logEvent;
        private readonly string _logInformation = "Question";


        /// <summary>
        /// Return a random question.
        /// </summary>
        /// <returns>A random question</returns>
        /// <response code="200">Returns the question</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Question not found</response>
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            try
            {
                var questions = await _questionService.GetListAllQuestion();
                if (questions == null)
                {
                    _logger.LogWarning(_logEvent.Value.GetItem, "{Question} : There is no question found", _logInformation);
                    return NotFound("There is no question");
                }
                _logger.LogInformation(_logEvent.Value.GetItem, "{Question} : List of all questions returned", _logInformation);
                return Ok(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError("Question : Server error at get list question");
                return BadRequest(ex);
            }
            
        }

        /// <summary>
        /// Get question by id.
        /// </summary>
        /// <param name="id">Id of the question</param>
        /// <returns>The specified question</returns>
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult> GetQuestion(Guid id)
        {
            try
            {
                var question = await _questionService.GetQuestionById(id);
                if (question == null)
                {
                    _logger.LogWarning(_logEvent.Value.GetItem, "{Question} : There is no question found", _logInformation);
                    return NotFound("There is no question");
                }
                _logger.LogInformation(_logEvent.Value.GetItem, "{Question} : List of all questions returned", _logInformation);
                return Ok(question);
            }
            catch (Exception ex)
            {
                _logger.LogError("Question : Server error at get list question");
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Creates a Question with string proposition.
        /// </summary>
        /// <param name="question">Question model</param>
        /// <returns>A newly created Question</returns>
        /// <response code="201">Returns the newly created question</response>
        /// <response code="400">No files send or no question send</response>
        [HttpPost, Authorize]
        public IActionResult PostQuestionTexte([FromBody] Question question)            
        {
            try
            {
                var newQuestion =  _questionService.CreateQuestion(question);
                // _logger.LogInformation(_logEvent.Value.CreateItem, "{Question} : A new question text was created", _logInformation);
                // return CreatedAtAction("GetQuestion", new { id = newQuestion.Id}, newQuestion);
                return Ok(newQuestion);
            }
            catch(Exception ex)
            {
                _logger.LogError(_logEvent.Value.CreateItem, "Server error at question text creation", _logInformation);
                return BadRequest(ex);
            }
        }


        /// <summary>
        /// Creates a Question with media propositions.
        /// </summary>
        /// <param name="request">Form-data of the request with files and data</param>
        /// <param name="media">Media type[Audio;Image]</param>
        /// <returns>A newly created Question</returns>
        /// <response code="201">Returns the newly created question</response>
        /// <response code="400">No files send or no question send</response>
        [HttpPost("{media}"), Authorize]
        public async Task<IActionResult> PostQuestionMedia(IFormCollection request, MediaType media)

        {
            try
            {
                if(!request.ContainsKey("question"))
                {
                    _logger.LogWarning(_logEvent.Value.CreateItem, "{Question} : A new question media was created", _logInformation);
                    return BadRequest("No question sended");
                }
                if (request.Files.Count == 0)
                {
                    _logger.LogWarning(_logEvent.Value.CreateItem, "{Question}: No file sended", _logInformation);
                    return BadRequest("No file sended");
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
                _logger.LogInformation(_logEvent.Value.CreateItem, "A new question media was created", _logInformation);
                return CreatedAtAction("GetQuestion", new { id = newQuestion.Id }, newQuestion);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logEvent.Value.CreateItem, "Server error at question media creation", _logInformation);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Delete a question.
        /// </summary>
        /// <param name="id">Id of the question</param>
        /// <response code="200">The question has been successfully deleted</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Question not found</response>
        [HttpDelete, Authorize]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            try
            {
                var question = await _questionService.DeleteQuestion(id);
                if (!question)
                {
                    _logger.LogWarning(_logEvent.Value.CreateItem,"{Question} : the question id: {id} doesn't not exist", id, _logInformation);
                    return NotFound("The question doesn't not exist");
                }
                _logger.LogInformation(_logEvent.Value.CreateItem, "{Question} : the question {id} was successfully deleted", id, _logInformation);
                return Ok("The question was successfully deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(_logEvent.Value.CreateItem, "{Question} : Server error at question deleting", _logInformation);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Get random question.
        /// </summary>
        /// <returns>Random question</returns>
        [HttpGet]
        public async Task<ActionResult<QuestionDto>> GetRandomQuestion()
        {
            try
            {
                var question = await _questionService.GetRandomQuestion();
                if (question == null)
                {
                    _logger.LogWarning(_logEvent.Value.GetItem, "{Question} : There is no question found", _logInformation);
                    return NotFound("There is no question");
                }
                _logger.LogInformation(_logEvent.Value.GetItem, "{Question} : List of all questions returned", _logInformation);
                return Ok(question);
            }
            catch (Exception ex)
            {
                _logger.LogError("Question : Server error at get list question");
                return BadRequest(ex);
            }
        }

    }
}
