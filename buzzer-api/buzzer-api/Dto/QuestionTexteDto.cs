using buzzerApi.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Dto
{
    public class QuestionTexteDto
    {
        public Guid Id { get; set; }
        public string Interogation { get; set; }
        public int Reponse { get; set; }
        public QuestionType QuestionType { get; set; }
        public MediaQuestion Media { get; set; }
        public virtual IEnumerable<PropositionDto> Propositions { get; set; }
    }

    public static class QuestionTexteExtensions
    {
        public static QuestionTexteDto ToDto(this Models.Question entity)
        {
            return new QuestionTexteDto
            {
                Id = entity.Id,
                Interogation = entity.Interogation,
                Reponse = entity.Propositions.IndexOf(entity.Propositions.First(x => x.IsCorrect)),
                QuestionType = entity.QuestionType,
                Propositions = entity.Propositions.Select(x => x.ToDto()),
                Media = (entity.Propositions.Count == 0 || entity.Propositions == null) ? null : entity.MediaQuestions.First()
            };
        }
    }
}
