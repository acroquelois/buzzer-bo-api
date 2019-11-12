using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Models
{
    public class Propositions
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
        public Guid QuestionId { get; set; }

        [Required]
        public string proposition { get; set; }
    }
}
