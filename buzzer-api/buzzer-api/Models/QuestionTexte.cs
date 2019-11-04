using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Models
{
    public class QuestionTexte
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string Reponse { get; set; }

        [Required]
        public string Proposition1 { get; set; }

        [Required]
        public string Proposition2 { get; set; }

        [Required]
        public string Proposition3 { get; set; }

        [Required]
        public string Proposition4 { get; set; }
    }
}
