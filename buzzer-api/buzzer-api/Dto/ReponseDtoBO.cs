using buzzerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Dto
{
    public class ReponseDtoBO
    {
        public Guid Id { get; set; }

        public string Reponse { get; set; }

        public Guid QuestionId { get; set; }

    }
    public static class ReponseDtoBOExtensions
    {
        public static Propositions ToProposition(this ReponseDtoBO reponse)
        {
            return new Propositions
            {
                Id = reponse.Id,
                Proposition = reponse.Reponse,
                IsCorrect = true,
                QuestionId = reponse.QuestionId
            };
        }

        public static ReponseDtoBO ToReponse(this PropositionDtoBO proposition)
        {
            return new ReponseDtoBO
            {
                Id = proposition.Id,
                Reponse = proposition.Proposition,
                QuestionId = proposition.QuestionId
            };
        }
    }
}
