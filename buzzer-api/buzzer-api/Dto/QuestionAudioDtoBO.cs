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
    public class QuestionAudioDtoBO
    {
        public Guid Id { get; set; }
        public string Interogation { get; set; }
        public ReponseDtoBO Reponse { get; set; }
        public QuestionType QuestionType { get; set; }
        public MediaQuestionDto Media{ get; set; }
    }

    public static class QuestionAudioDtoBOExtensions
    {
        public static QuestionAudioDtoBO ToDto(this Models.Question entity)
        {
            return new QuestionAudioDtoBO
            {
                Id = entity.Id,
                Interogation = entity.Interogation,
                Reponse = entity.Propositions == null ? null : (PropositionDtoBOExtensions.ToDto(entity.Propositions.First(x => x.IsCorrect))).ToReponse(),
                Media = (entity.MediaQuestions.Count == 0) ? null : entity.MediaQuestions.First().ToDto(),
                QuestionType = entity.QuestionType
            };
        }
        public static Question ToEntity(this QuestionAudioDtoBO dto)
        {
            if (dto.Reponse == null)
            {
                throw new NoResponseException("La question ne contient pas de réponse");
            }
            ICollection<Propositions> propositions = new List<Propositions>();
            propositions.Add(dto.Reponse.ToProposition());
            return new Question
            {
                Id = dto.Id,
                Interogation = dto.Interogation,
                QuestionTypeId = dto.QuestionType == null ? null : dto.QuestionType.Id,
                Propositions = propositions,
                MediaQuestions = dto.Media == null ? new List<MediaQuestion>() : new List<MediaQuestion> { dto.Media.ToEntity() }
            };
        }
    }
}
