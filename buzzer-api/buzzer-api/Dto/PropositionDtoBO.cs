using buzzerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Dto
{
    public class PropositionDtoBO
    {
        public Guid Id { get; set; }
        public string Proposition { get; set; }
        public Guid QuestionId { get; set; }
    }

    public static class PropositionDtoBOExtensions
    {
        public static PropositionDtoBO ToDto(this Models.Propositions entity)
        {
            return new PropositionDtoBO
            {
                Id = entity.Id,
                Proposition = entity.Proposition,
                QuestionId = entity.QuestionId
            };
        }

        public static Propositions ToEntity(this PropositionDtoBO dto)
        {
            return new Propositions
            {
                Id = dto.Id,
                Proposition = dto.Proposition,
                IsCorrect = false,
                QuestionId = dto.QuestionId
            };
        }
    }
}
