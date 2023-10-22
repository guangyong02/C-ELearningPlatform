using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningPlatform
{
    internal class Quiz: ILearningMaterials
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
            Console.WriteLine(Title + " By " +MadeBy+ " with " +Difficulty+" difficulty.");
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
        public override string ToString()
        {
            return Title;
        }

        public void Learning(Student currStudent)
        {
            Console.WriteLine(Title);
            Console.WriteLine();
            int correct = 0;
            foreach (Question question in Question)
            {
                Console.WriteLine(question.Topic);
                Console.Write("What is your answer :");
                if (question.Answer.Equals(Console.ReadLine()))
                    correct++;
            }
            Program.ClearScreen();
            double finalScore = (double)correct / Question.Count * 100;
            Console.WriteLine("Average Score is : {0}%", Math.Round(finalScore,2));
            if (finalScore > HighScore)
            {
                Console.WriteLine("You have become the high score holder with {0}%", Math.Round(finalScore, 2));
                HighScore = finalScore;
                HighScoreHolder = currStudent;
            }
            else
                Console.WriteLine("Keep it up {0}!",currStudent.Username);
            currStudent.FinishAQuiz(Title, finalScore);
            Console.WriteLine();
        }
    }
}
