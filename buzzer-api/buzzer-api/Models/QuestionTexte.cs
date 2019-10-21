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
        public string question { get; set; }

        [Required]
        public string reponse { get; set; }

        [Required]
        public string proposition1 { get; set; }

        [Required]
        public string proposition2 { get; set; }

        [Required]
        public string proposition3 { get; set; }

        [Required]
        public string proposition4 { get; set; }
    }
}
