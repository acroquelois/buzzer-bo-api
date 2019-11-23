using buzzerApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace buzzerApi.Services.Abstraction
{
    public interface IQuestionService
    {
        Task<Question> CreateQuestionTexte(Question question);

        Task<IEnumerable<Question>> GetListAllQuestionTexte();

        Task<bool> DeleteQuestion(Guid id);
    }
}
