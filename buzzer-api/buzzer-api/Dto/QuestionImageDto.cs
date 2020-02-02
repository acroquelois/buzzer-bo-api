using buzzerApi.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Dto
{
    public class QuestionImageDto
    {
        public Guid Id { get; set; }
        public string Interogation { get; set; }
        public string Reponse { get; set; }
        public QuestionType QuestionType { get; set; }
        public virtual IEnumerable<MediaQuestionDto> Medias { get; set; }
    }

    public static class QuestionImageDtoExtensions
    {
        public static QuestionImageDto ToDto(this Models.Question entity)
        {
            return new QuestionImageDto
            {
                Id = entity.Id,
                Interogation = entity.Interogation,
                Reponse = (entity.Propositions.Count == 0 || entity.Propositions == null) ? null : entity.Propositions.First(x => x.IsCorrect) == null ? null : entity.Propositions.First(x => x.IsCorrect).Proposition,
                QuestionType = entity.QuestionType,
                Medias = entity.MediaQuestions.Select(x => x.ToDto()).ToList()
            };
        }
    }
}
