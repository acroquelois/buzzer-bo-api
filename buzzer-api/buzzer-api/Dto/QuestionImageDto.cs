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
        public virtual IEnumerable<MediaQuestion> Medias { get; set; }
    }

    public static class QuestionImageExtensions
    {
        public static QuestionImageDto ToDto(this Models.Question entity)
        {
            return new QuestionImageDto
            {
                Id = entity.Id,
                Interogation = entity.Interogation,
                Reponse = (entity.Propositions.Count == 0 || entity.Propositions == null) ? null : entity.Propositions.First(x => x.IsCorrect) == null ? null : entity.Propositions.First(x => x.IsCorrect).proposition,
                QuestionType = entity.QuestionType,
                Medias = entity.MediaQuestions
            };
        }
    }
}
