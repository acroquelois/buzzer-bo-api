using System;
using System.ComponentModel.DataAnnotations;

namespace buzzerApi.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
