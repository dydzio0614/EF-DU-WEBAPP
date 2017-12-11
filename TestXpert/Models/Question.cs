using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public Answer Answers { get; set; } //dumbed down for now from ICollection<Answer> for debug purposes - ordered list of answer contents
        [Required]
        public int Points { get; set; }
        [Required]
        public int CorrectAnswer { get; set; } //subject to change when system grows (multiple correct answers available etc. Database change not a big deal while app still in development
    }
}
