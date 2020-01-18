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

        public async Task<Question> GetQuestionById(Guid id)
        {
            return await _repository.GetQuestion(id);
        }
        public async Task<IEnumerable<Question>> GetListAllQuestion()
        {
            return await _repository.ListAllQuestionTexte();
        }

        public async Task<bool> DeleteQuestion(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<Question> GetRandomQuestion()
        {
            return await _repository.GetRandomQuestion();
        }
    }
}
