using buzzerApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace buzzerApi.Services.Abstraction
{
    public interface IQuestionService
    {
        Task<Question> CreateQuestion(Question question);

        Task<IEnumerable<Question>> GetListAllQuestion();

        Task<bool> DeleteQuestion(Guid id);
    }
}
