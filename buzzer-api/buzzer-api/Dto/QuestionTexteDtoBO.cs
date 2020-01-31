using buzzerApi.Models;
using buzzerApi.Dto;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buzzerApi.Exceptions;
using buzzerApi.Enum;

namespace buzzerApi.Dto
{
    public class QuestionTexteDtoBO
    {
        public Guid Id { get; set; }
        public string Interogation { get; set; }
        public ReponseDtoBO Reponse { get; set; }
        public ICollection<MediaQuestion> MediaQuestions { get; set; } = new List<MediaQuestion>();
        public ICollection<PropositionDtoBO> Propositions { get; set; } = new List<PropositionDtoBO>();
    }

    public static class QuestionTexteDtoBOExtensions
    {
        public static QuestionTexteDtoBO ToDto(this Models.Question entity)
        {
            return new QuestionTexteDtoBO
            {
                Id = entity.Id,
                Interogation = entity.Interogation,
                Reponse = entity.Propositions == null ? null : (PropositionDtoBOExtensions.ToDto(entity.Propositions.First(x => x.IsCorrect))).ToReponse(),
                Propositions = entity.Propositions.Where(x => !x.IsCorrect).Select(x => PropositionDtoBOExtensions.ToDto(x)).ToList(),
                MediaQuestions= entity.MediaQuestions
            };
        }
        public static Question ToEntity(this QuestionTexteDtoBO dto)
        {
            if (dto.Reponse == null)
            {
                throw new NoResponseException("La question ne contient pas de réponse");
            }
            if(dto.Propositions.Count != 3)
            {
                throw new NoPropositionException("La question doit contenir trois fausses proposition de réponse");
            }
            ICollection<Propositions> propositions = dto.Propositions.Select(x => PropositionDtoBOExtensions.ToEntity(x)).ToList();
            propositions.Add(dto.Reponse.ToProposition());
            return new Question
            {
                Id = dto.Id,
                Interogation = dto.Interogation,
                QuestionTypeId = EnumQuestionType.TEXTE.ToString(),
                Propositions = propositions,
                MediaQuestions = dto.MediaQuestions
            };
        }
    }
}
