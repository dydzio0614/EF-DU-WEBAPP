using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestXpert.Models
{
    public class Question
    {
        private int ID;
        private string description; //content of the question
        private List<string> answers; //ordered list of answer contents
        private int points;
        private int correctAnswer; //subject to change when system grows (multiple correct answers available etc. Database change not a big deal while app still in development

        public Question(int id, string description, List<string> answers, int points, int correctAnswer)
        {
            ID = id;
            this.description = description;
            this.answers = answers;
            this.points = points > 0 ? points : 1;
            this.correctAnswer = correctAnswer;
        }

        public int Points
        {
            get { return points; }
            set { points = value; }
        }
    }
}
