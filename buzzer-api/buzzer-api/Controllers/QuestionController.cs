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
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class QuestionController : ControllerBase
    {

        public QuestionController(IQuestionService questionService, IOptions<UploadOptions> uploadOptions, IOptions<ConnectionOptions> connexionOptions, IUploadService uploadService, ILogger<QuestionController> logger, IOptions<LogEventOptions> logEvent)
        {
            _questionService = questionService;
            _uploadOptions = uploadOptions;
            _connexionOptions = connexionOptions;
            _uploadService = uploadService;
            _logger = logger;
            _logEvent = logEvent;

        }

        private readonly IQuestionService _questionService;
        private readonly IOptions<UploadOptions> _uploadOptions;
        private readonly IOptions<ConnectionOptions> _connexionOptions;
        private readonly IUploadService _uploadService;
        private readonly ILogger<QuestionController> _logger;
        private readonly IOptions<LogEventOptions> _logEvent;
        private readonly string _logInformation = "Question";


        /// <summary>
        /// Return a list of all questions.
        /// </summary>
        /// <returns>List of questions</returns>
        /// <response code="200">Returns a list of questions</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Question not found</response>
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetListQuestions()
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
        /// <response code="200">The question was returned</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Question not found</response>
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<QuestionDto>> GetQuestion(Guid id)
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
        /// Get question of type texte by id.
        /// </summary>
        /// <param name="id">Id of the question</param>
        /// <response code="200">The question was returned</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Question not found</response>
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<QuestionTexteDtoBO>> GetQuestionTexte(Guid id)
        {
            try
            {
                var question = await _questionService.GetQuestionTexteById(id);
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
        /// Get question of type image by id.
        /// </summary>
        /// <param name="id">Id of the question</param>
        /// <response code="200">The question was returned</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Question not found</response>
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<QuestionImageDtoBO>> GetQuestionImage(Guid id)
        {
            try
            {
                var question = await _questionService.GetQuestionImageById(id);
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
        /// Get question of type audio by id.
        /// </summary>
        /// <param name="id">Id of the question</param>
        /// <response code="200">The question was returned</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Question not found</response>
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<QuestionAudioDtoBO>> GetQuestionAudio(Guid id)
        {
            try
            {
                var question = await _questionService.GetQuestionAudioById(id);
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
        /// Creates a question.
        /// </summary>
        /// <param name="request">form-data input</param>
        /// <param name="questionType">type of the question: [TEXTE,AUDIO,IMAGE]</param>
        /// <returns>A newly created Question</returns>
        /// <response code="201">Returns the newly created question</response>
        /// <response code="400">No files send or no question send</response>
        [HttpPost("{questionType}"), Authorize]
        public async Task<IActionResult> PostQuestion(IFormCollection request, EnumQuestionType questionType)
        {
            try
            {
                if (!request.ContainsKey("question"))
                {
                    _logger.LogWarning(_logEvent.Value.CreateItem, $"Question : A new question of type {questionType.ToString()} was created", _logInformation);
                    return BadRequest("No question sended");
                }
                IFormFileCollection files = request.Files;
                string key = request.Keys.FirstOrDefault(x => x == "question");
                Question question = null;
                EnumMediaType mediaType;
                switch (questionType)
                {
                    case EnumQuestionType.TEXTE:
                        mediaType = EnumMediaType.Image;
                        question = JsonConvert.DeserializeObject<QuestionTexteDtoBO>(request[key]).ToEntity();
                        break;
                    case EnumQuestionType.IMAGE:
                        if (request.Files.Count != 3)
                        {
                            _logger.LogWarning(_logEvent.Value.CreateItem, "{Question}: Question image must contain three media", _logInformation);
                            return BadRequest("No file sended");
                        }
                        mediaType = EnumMediaType.Image;
                        question = JsonConvert.DeserializeObject<QuestionImageDtoBO>(request[key]).ToEntity();
                        break;
                    case EnumQuestionType.AUDIO:
                        if (request.Files.Count != 1)
                        {
                            _logger.LogWarning(_logEvent.Value.CreateItem, "{Question}: Question audio must contain only one media", _logInformation);
                            return BadRequest("No file sended");
                        }
                        mediaType = EnumMediaType.Audio;
                        question = JsonConvert.DeserializeObject<QuestionAudioDtoBO>(request[key]).ToEntity();
                        break;
                    default:
                        return BadRequest("Type de question inconnu");
                }
                Question newQuestion = await _questionService.CreateQuestion(question, files, mediaType);
                _logger.LogInformation(_logEvent.Value.CreateItem, $"A new question of type {questionType.ToString()} was created", _logInformation);
                return CreatedAtAction("GetQuestion", new { id = newQuestion.Id }, newQuestion);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logEvent.Value.CreateItem, $"Server error at question media creation{ex.ToString()}", _logInformation);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update a question.
        /// </summary>
        /// <param name="request">form-data input</param>
        /// <param name="questionType">type of the question: [TEXTE,AUDIO,IMAGE]</param>
        /// <returns>Updated question</returns>
        /// <response code="201">Returns the updated question</response>
        /// <response code="400">No files send or no question send</response>
        [HttpPost("{questionType}"), Authorize]
        public async Task<IActionResult> UpdateQuestion(IFormCollection request, EnumQuestionType questionType)
        {
            try
            {
                if (!request.ContainsKey("question"))
                {
                    _logger.LogWarning(_logEvent.Value.UpdateItem, $"Question : A new question of type {questionType.ToString()} was created", _logInformation);
                    return BadRequest("No question sended");
                }
                IFormFileCollection files = request.Files;
                string key = request.Keys.FirstOrDefault(x => x == "question");
                Question question = null;
                EnumMediaType mediaType;
                switch (questionType)
                {
                    case EnumQuestionType.TEXTE:
                        mediaType = EnumMediaType.Image;
                        question = JsonConvert.DeserializeObject<QuestionTexteDtoBO>(request[key]).ToEntity();
                        break;
                    case EnumQuestionType.IMAGE:
                        mediaType = EnumMediaType.Image;
                        question = JsonConvert.DeserializeObject<QuestionImageDtoBO>(request[key]).ToEntity();
                        break;
                    case EnumQuestionType.AUDIO:
                        mediaType = EnumMediaType.Audio;
                        question = JsonConvert.DeserializeObject<QuestionAudioDtoBO>(request[key]).ToEntity();
                        break;
                    default:
                        return BadRequest("Type de question inconnu");
                }
                Question newQuestion = await _questionService.UpdateQuestion(question, files, mediaType);
                _logger.LogInformation(_logEvent.Value.UpdateItem, "{Question} : The question with id {id} was modified", _logInformation, question.Id);
                return Ok(newQuestion);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logEvent.Value.CreateItem, $"Server error at question media creation{ex.ToString()}", _logInformation);
                return BadRequest(ex.Message);
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
        /// Get random question texte
        /// </summary>
        /// <response code="200">A Random question texte was returned</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Question not found</response>
        [HttpGet]
        public async Task<ActionResult<QuestionTexteDto>> GetRandomQuestionTexte()
        {
            try
            {
                var question = await _questionService.GetRandomQuestionTexte();
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
                _logger.LogError("Question : Server error at get random question");
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Get random question image
        /// </summary>
        /// <response code="200">A Random question image was returned</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Question not found</response>
        [HttpGet]
        public async Task<ActionResult<QuestionImageDto>> GetRandomQuestionImage()
        {
            try
            {
                var question = await _questionService.GetRandomQuestionImage();
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
                _logger.LogError("Question : Server error at get random question");
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Get random question audio
        /// </summary>
        /// <response code="200">A Random question audio was returned</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Question not found</response>
        [HttpGet]
        public async Task<ActionResult<QuestionAudioDto>> GetRandomQuestionAudio()
        {
            try
            {
                var question = await _questionService.GetRandomQuestionAudio();
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
                _logger.LogError("Question : Server error at get random question");
                return BadRequest(ex);
            }
        }

    }
}
