using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningPlatform
{
    internal class Subject
    {
        public string ShortForm {  get; set; }
        public string SubjectTitle { get; set; }
        public string SubjectDescription { get; set; }
        public List<Quiz> Quizzes { get; set; }

        //public List<Lesson>
        

        //public Teacher HighScoreHolderTeacher { get; set; }

        public Subject(string shortForm,string subjectTitle, string subjectDescription)
        {
            ShortForm = shortForm;
            SubjectTitle = subjectTitle;
            SubjectDescription = subjectDescription;
            Quizzes= new List<Quiz>();
        }
        public void AddQuizz(Quiz quiz)
        {
            Quizzes.Add(quiz);
        }

        public void ShowDetails()
        {
            Console.WriteLine($"Subject : {SubjectTitle} ({ShortForm})");
            Console.WriteLine("Quizz include");
            for (int i = 0; i < Quizzes.Count; i++)
            {
                Console.Write(i + 1 +". ");
                Quizzes[i].ShowDetails();
            }
        }


        public override string ToString()
        {
            return" is the high score holder for " + SubjectTitle +
                " with score ";
        }



    }
}
