using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestXpert.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; } //content of the question
        [Required]
        public ICollection<Answer> Answers { get; set; }
        [Required]
        public int Points { get; set; }
        [Required]
        public int CorrectAnswer { get; set; } //subject to change when system grows (multiple correct answers available etc. Database change not a big deal while app still in development
        [ForeignKey("ApplicationUser")]
        public ApplicationUser RelatedUser { get; set; }
    }
}
