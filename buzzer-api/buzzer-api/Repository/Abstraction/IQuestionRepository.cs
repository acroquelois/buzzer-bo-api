using buzzerApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace buzzerApi.Repository.Abstraction
{
    public interface IQuestionRepository
    {
        Task CreateAsync(Question question);
        IEnumerable<Question> ListAllQuestionTexte();
        Task<bool> DeleteAsync(Guid id);
        Question ListOneQuestionTexte(Guid id);
        Question ListRandomQuestionTexte();
    }
}
