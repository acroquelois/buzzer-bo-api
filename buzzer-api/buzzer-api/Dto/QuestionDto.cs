using buzzerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Dto
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public string Interogation { get; set; }
        public int Reponse { get; set; }
        public QuestionType QuestionType { get; set; }
        public virtual IEnumerable<PropositionDto> Propositions { get; set; }
    }

    public static class QuestionExtensions
    {
        public static QuestionDto ToDto(this Models.Question entity)
        {
            return new QuestionDto
            {
                Id = entity.Id,
                Interogation = entity.Interogation,
                Reponse = entity.Reponse,
                QuestionType = entity.QuestionType,
                Propositions = entity.Propositions.Select(x => x.ToDto())
            };
        }
    }
}
