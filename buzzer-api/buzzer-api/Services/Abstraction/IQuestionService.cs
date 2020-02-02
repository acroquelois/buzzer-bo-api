using buzzerApi.Dto;
using buzzerApi.Enum;
using buzzerApi.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace buzzerApi.Services.Abstraction
{
    public interface IQuestionService
    {

        Task<Question> CreateQuestion(Question question, IFormFileCollection files, EnumMediaType mediaType);
        Task<Question> UpdateQuestion(Question question, IFormFileCollection files, EnumMediaType mediaType);
        Task<QuestionDto> GetQuestionById(Guid id);
        Task<QuestionTexteDtoBO> GetQuestionTexteById(Guid id);
        Task<QuestionImageDtoBO> GetQuestionImageById(Guid id);
        Task<QuestionAudioDtoBO> GetQuestionAudioById(Guid id);
        Task<IEnumerable<QuestionDto>> GetListAllQuestion();
        Task<bool> DeleteQuestion(Guid id);
        Task<QuestionTexteDto> GetRandomQuestionTexte();
        Task<QuestionImageDto> GetRandomQuestionImage(); 
        Task<QuestionAudioDto> GetRandomQuestionAudio();
    }
}
