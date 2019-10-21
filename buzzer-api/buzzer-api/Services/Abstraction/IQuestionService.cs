using buzzerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Services.Abstraction
{
    public interface IQuestionService
    {
        Task CreateQuestionTexte(QuestionTexte question);

        Task<IEnumerable<QuestionTexte>> GetListAllQuestionTexte();
    }
}
