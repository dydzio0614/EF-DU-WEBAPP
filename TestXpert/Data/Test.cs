using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestXpert.Data
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } //test name
        [Required]
        public ICollection<Question> Questions { get; set; }

        /*public Test()
        {
            Questions = new List<Question>();
        }

        public int GetMaxPoints()
        {
            int sum = 0;
            foreach (var q in Questions)
                sum += q.Points;
            return sum;
        }

        public void AddQuestion(int id, string description, List<string> answers, int points, int correctAnswer)
        {
            Questions.Add(new Question(id, description, answers, points, correctAnswer));
        }*/
    }
}
