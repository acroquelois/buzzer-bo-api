using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
