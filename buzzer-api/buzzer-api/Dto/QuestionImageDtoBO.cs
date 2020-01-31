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
    public class QuestionImageDtoBO
    {
        public Guid Id { get; set; }
        public string Interogation { get; set; }
        public ReponseDtoBO Reponse { get; set; }
        public ICollection<MediaQuestion> MediaQuestions { get; set; } = new List<MediaQuestion>();
    }

    public static class QuestionImageDtoBOExtensions
    {
        public static QuestionImageDtoBO ToDto(this Models.Question entity)
        {
            return new QuestionImageDtoBO
            {
                Id = entity.Id,
                Interogation = entity.Interogation,
                Reponse = entity.Propositions == null ? null : (PropositionDtoBOExtensions.ToDto(entity.Propositions.First(x => x.IsCorrect))).ToReponse(),
                MediaQuestions = entity.MediaQuestions
            };
        }
        public static Question ToEntity(this QuestionImageDtoBO dto)
        {
            if (dto.Reponse == null)
            {
                throw new NoResponseException("La question ne contient pas de réponse");
            }
            if (dto.MediaQuestions.Count != 3)
            {
                throw new NoMediaException("La question doit contenir trois images");
            }
            ICollection<Propositions> propositions = new List<Propositions>();
            propositions.Add(dto.Reponse.ToProposition());
            return new Question
            {
                Id = dto.Id,
                Interogation = dto.Interogation,
                QuestionTypeId = EnumQuestionType.IMAGE.ToString(),
                Propositions = propositions,
                MediaQuestions = dto.MediaQuestions
            };
        }
    }
}
