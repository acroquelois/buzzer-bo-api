using buzzerApi.Dto;
using buzzerApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace buzzerApi.Services.Abstraction
{
    public interface IQuestionService
    {
        Task<Question> CreateQuestion(Question question);

        Task<Question> UpdateQuestion(Question question);

        Task<QuestionDto> GetQuestionById(Guid id);

        Task<IEnumerable<Question>> GetListAllQuestion();

        Task<bool> DeleteQuestion(Guid id);

        Task<QuestionTexteDto> GetRandomQuestionTexte();
        Task<QuestionImageDto> GetRandomQuestionImage(); 
        Task<QuestionImageDto> GetRandomQuestionAudio();
    }
}
