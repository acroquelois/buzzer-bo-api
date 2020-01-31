using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace buzzerApi.Models
{
    public class Propositions
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
        public Guid QuestionId { get; set; }
        [DefaultValue("false")]
        public bool IsCorrect { get; set; }
        [Required]
        public string Proposition { get; set; } 
    }
}
