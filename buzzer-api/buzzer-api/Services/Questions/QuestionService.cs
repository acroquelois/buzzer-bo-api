using buzzerApi.Dto;
using buzzerApi.Models;
using buzzerApi.Repository.Abstraction;
using buzzerApi.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace buzzerApi.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _repository;

        public QuestionService(IQuestionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Question> CreateQuestion(Question question)
        { 
            await _repository.CreateAsync(question);
            return question;
        }

        public async Task<Question> UpdateQuestion(Question question)
        {
            await _repository.UpdateQuestion(question);
            return question;
            
        }

        public async Task<QuestionDto> GetQuestionById(Guid id)
        {
            var entity = await _repository.GetQuestion(id);
            return QuestionExtensions.ToDto(entity);
        }
        public async Task<IEnumerable<Question>> GetListAllQuestion()
        {
            return await _repository.ListAllQuestion();
        }

        public async Task<bool> DeleteQuestion(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<QuestionTexteDto> GetRandomQuestionTexte()
        {
            var question = await _repository.GetRandomQuestionTexte();
            return QuestionTexteExtensions.ToDto(question);
        }

        public async Task<QuestionImageDto> GetRandomQuestionImage()
        {
            var question = await _repository.GetRandomQuestionImage();
            return QuestionImageExtensions.ToDto(question);
        }
        public async Task<QuestionImageDto> GetRandomQuestionAudio()
        {
            var question = await _repository.GetRandomQuestionAudio();
            return QuestionImageExtensions.ToDto(question);
        }
    }
}
