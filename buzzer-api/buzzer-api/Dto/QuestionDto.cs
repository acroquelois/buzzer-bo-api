using buzzerApi.Models;
using buzzerApi.Dto;
using Microsoft.EntityFrameworkCore.Internal;
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
        public string Reponse { get; set; }
        public QuestionType QuestionType { get; set; }
        public IEnumerable<MediaQuestionDto> MediaQuestion { get; set; }
        public IEnumerable<PropositionDto> Propositions { get; set; }
    }

    public static class QuestionDtoExtensions
    {
        public static QuestionDto ToDto(this Models.Question entity)
        {
            return new QuestionDto
            {
                Id = entity.Id,
                Interogation = entity.Interogation,
                Reponse = entity.Propositions.Count == 0 ? null : entity.Propositions.First(x => x.IsCorrect).Proposition,
                QuestionType = entity.QuestionType,
                Propositions = entity.Propositions.Select(x => PropositionDtoExtensions.ToDto(x)),
                MediaQuestion = entity.MediaQuestions.Select(x => x.ToDto())
            };
        }
    }
}