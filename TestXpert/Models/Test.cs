using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestXpert.Models
{
    public class Test
    {
        private int ID;
        private string name; //test name
        private List<Question> questions;

        public Test()
        {
            questions = new List<Question>();
        }

        public int GetMaxPoints()
        {
            int sum = 0;
            foreach (var q in questions)
                sum += q.Points;
            return sum;
        }

        public void AddQuestion(int id, string description, List<string> answers, int points, int correctAnswer)
        {
            questions.Add(new Question(id, description, answers, points, correctAnswer));
        }
    }
}
