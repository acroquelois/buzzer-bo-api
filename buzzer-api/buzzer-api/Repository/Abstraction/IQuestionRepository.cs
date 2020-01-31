using buzzerApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace buzzerApi.Repository.Abstraction
{
    public interface IQuestionRepository

    {
        Task CreateAsync(Question question);
        Task UpdateQuestion(Question question);
        Task<Question> GetQuestion(Guid id);
        Task<IEnumerable<Question>> ListAllQuestion();
        Task<IEnumerable<Question>> ListAllQuestionTexte();
        Task<IEnumerable<Question>> ListAllQuestionImage(); 
        Task<bool> DeleteAsync(Guid id);
    }
}
