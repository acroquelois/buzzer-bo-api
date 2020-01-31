using buzzerApi.Dto;
using buzzerApi.Enum;
using buzzerApi.Models;
using buzzerApi.Options;
using buzzerApi.Repository.Abstraction;
using buzzerApi.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buzzerApi.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _repository;
        private readonly IUploadService _uploadService;
        private readonly IOptions<UploadOptions> _uploadOptions;
        private readonly IOptions<ConnectionOptions> _connexionOptions;

        public QuestionService(IQuestionRepository repository, IUploadService uploadService, IOptions<UploadOptions> uploadOptions, IOptions<ConnectionOptions> connexionOptions)
        {
            _repository = repository;
            _uploadService = uploadService;
            _uploadOptions = uploadOptions;
            _connexionOptions = connexionOptions;
        }

        public async Task<Question> CreateQuestion(Question question, IFormFileCollection files, EnumMediaType mediaType)
        {

            ICollection<string> pathFiles = await _uploadService.UploadMedia(_uploadOptions, _connexionOptions, mediaType, files);

            ICollection<MediaQuestion> medias = new List<MediaQuestion>();
            foreach (var path in pathFiles)
            {
                MediaQuestion media = new MediaQuestion();
                media.Url = path;
                media.MediaType = mediaType;
                medias.Add(media);
            }
            question.MediaQuestions = medias;
            question.Propositions = question.Propositions;
            await _repository.CreateAsync(question);
            return question;
        }


        public async Task<Question> UpdateQuestion(Question question)
        {
            await _repository.UpdateQuestion(question);
            return question;
            
        }

        public async Task<bool> DeleteQuestion(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }


        public async Task<QuestionDto> GetQuestionById(Guid id)
        {
            Question question = await _repository.GetQuestion(id);
            return QuestionDtoExtensions.ToDto(question);
        }

        public async Task<QuestionTexteDtoBO> GetQuestionTexteById(Guid id)
        {
            var entity = await _repository.GetQuestion(id);
            return QuestionTexteDtoBOExtensions.ToDto(entity);
        }

        public async Task<IEnumerable<QuestionDto>> GetListAllQuestion()
        {
            IEnumerable<Question> listQuestions = await _repository.ListAllQuestion();
            return listQuestions.Select(x => QuestionDtoExtensions.ToDto(x));
        }

        private Question GetRandomQuestion(IEnumerable<Question> questions)
        {
            try
            {
                List<Question> question_list = new List<Question>();
                question_list.AddRange(questions);
                Random rand = new Random();
                int random_number = rand.Next(0, question_list.Count);
                Question question = question_list[random_number];
                List<Guid> guid_list = new List<Guid>();
                guid_list.Add(question.Id);
                return question;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<QuestionTexteDto> GetRandomQuestionTexte()
        {
            try
            {
                Question question = this.GetRandomQuestion(await _repository.ListAllQuestionTexte());
                return QuestionTexteDtoExtensions.ToDto(question);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<QuestionImageDto> GetRandomQuestionImage()
        {
            try
            {
                Question question = this.GetRandomQuestion(await _repository.ListAllQuestionImage());
                return QuestionImageDtoExtensions.ToDto(question);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<Question> GetRandomQuestionAudio()
        //{
        //    try
        //    {
        //        return this.GetRandomQuestion(await _repository.ListAllQuestionAudio());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
