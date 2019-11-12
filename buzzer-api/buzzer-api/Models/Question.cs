﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Models
{
    public class Question
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Interogation { get; set; }

        [Required]
        public string Reponse { get; set; }

        [ForeignKey("QuestionTypeId")]
        public QuestionType QuestionType { get; set; }
        public string QuestionTypeId { get; set; }

        [Required]
        public virtual ICollection<Propositions> Propositions { get; set; }
    }
}