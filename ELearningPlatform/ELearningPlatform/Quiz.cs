using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningPlatform
{
    internal class Quiz
    {
        public string Title {  get; set; }
        public Teacher MadeBy { get; }
        public double Difficulty { get; set; }
        public List<Question> Question;

        public double HighScore { get; set; }
        public Student? HighScoreHolder { get; set; }


        public Quiz(string title, Teacher madeBy, double difficulty)
        {
            Title = title;
            MadeBy = madeBy;
            Difficulty = difficulty;
            Question = new List<Question>();
            HighScore = 0;
            HighScoreHolder = null;
        }

        public void AddQuestion(Question question)
        {
            Question.Add(question);
        }

        public void ShowDetails()
        {
            Console.WriteLine(Title + " By " +MadeBy+ " with " +Difficulty+" difficulty");
            if (HighScoreHolder != null)
            {
                Console.WriteLine("High Score Holder is " + HighScoreHolder.ToString() + " with " + HighScore);
            }
        }
        public void ShowQuestion()
        {
            for (int i = 0; i < Question.Count; i++)
            {
                Console.WriteLine(i+1 +". " + Question[i].Topic);
            }
        }


    }
}
