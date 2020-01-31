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

    public static class QuestionTexteDtoExtensions
    {
        public static QuestionTexteDto ToDto(this Models.Question entity)
        {
            return new QuestionTexteDto
            {
                Id = entity.Id,
                Interogation = entity.Interogation,
                Reponse = entity.Propositions.IndexOf(entity.Propositions.First(x => x.IsCorrect)),
                QuestionType = entity.QuestionType,
                Propositions = entity.Propositions.Select(x => PropositionDtoExtensions.ToDto(x)),
                Media = (entity.MediaQuestions.Count == 0) ? null : entity.MediaQuestions.First()
            };
        }
    }
}
