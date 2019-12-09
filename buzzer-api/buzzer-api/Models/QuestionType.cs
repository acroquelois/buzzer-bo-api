using System.ComponentModel.DataAnnotations;

namespace buzzerApi.Models
{
    public class QuestionType
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
