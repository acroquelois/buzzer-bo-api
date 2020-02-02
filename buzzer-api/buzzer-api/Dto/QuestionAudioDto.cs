using buzzerApi.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Dto
{
    public class QuestionAudioDto
    {
        public Guid Id { get; set; }
        public string Interogation { get; set; }
        public string Reponse { get; set; }
        public QuestionType QuestionType { get; set; }
        public MediaQuestionDto Media { get; set; }
    }

    public static class QuestionAudioDtoExtensions
    {
        public static QuestionAudioDto ToDto(this Models.Question entity)
        {
            return new QuestionAudioDto
            {
                Id = entity.Id,
                Interogation = entity.Interogation,
                Reponse = (entity.Propositions.Count == 0 || entity.Propositions == null) ? null : entity.Propositions.First(x => x.IsCorrect) == null ? null : entity.Propositions.First(x => x.IsCorrect).Proposition,
                QuestionType = entity.QuestionType,
                Media = (entity.MediaQuestions.Count == 0) ? null : entity.MediaQuestions.First().ToDto()
            };
        }
    }
}
