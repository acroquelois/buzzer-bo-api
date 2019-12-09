using buzzerApi.Models;
using buzzerApi.Repository.Abstraction;
using buzzerApi.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task CreateQuestionTexte(Question question)
        {
            await _repository.CreateAsync(question);
        }

        public IEnumerable<Question> GetListAllQuestionTexte()
        {
            return _repository.ListAllQuestionTexte();
        }

        public async Task<bool> DeleteQuestion(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public Question GetListOneQuestionTexte(Guid id)
        {
            return _repository.ListOneQuestionTexte(id);
        }

        public Question GetListRandomQuestionTexte()
        {
            return _repository.ListRandomQuestionTexte();
        }
    }
}
