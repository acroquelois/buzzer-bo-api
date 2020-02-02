using buzzerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Dto
{
    public class MediaQuestionDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public Guid QuestionId { get; set; }
        public Enum.EnumMediaType MediaType { get; set; }
    }

    public static class MediaQuestionDtoExtensions
    {
        public static MediaQuestionDto ToDto(this Models.MediaQuestion entity)
        {
            return new MediaQuestionDto
            {
                Id = entity.Id,
                Url = entity.Url,
                QuestionId = entity.QuestionId,
                MediaType = entity.MediaType
            };
        }

        public static MediaQuestion ToEntity(this MediaQuestionDto dto)
        {
            return new MediaQuestion
            {
                Id = dto.Id,
                Url = dto.Url,
                QuestionId = dto.QuestionId,
                MediaType = dto.MediaType
            };
        }
    }
}
