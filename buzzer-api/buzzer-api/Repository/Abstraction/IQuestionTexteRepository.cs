using buzzerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Repository.Abstraction
{
    public interface IQuestionTexteRepository : IQuestionRepository
    {
        Task<IEnumerable<QuestionTexte>> ListAllQuestionTexte();
        Task<bool> DeleteAsync(Guid id);
    }
}
