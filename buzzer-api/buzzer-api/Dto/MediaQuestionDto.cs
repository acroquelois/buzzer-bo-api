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
        public Enum.MediaType MediaType { get; set; }
    }

    public static class MediaQuestionExtensions
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
    }
}
