using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningPlatform
{
    internal class Subject
    {
        public string ShortForm {  get; }
        public string SubjectTitle { get; }
        public string SubjectDescription { get; set; }
        public List<Quiz> Quizzes { get;}
        public List<Lesson> Lessons { get; }


        public Subject(string shortForm,string subjectTitle, string subjectDescription)
        {
            ShortForm = shortForm;
            SubjectTitle = subjectTitle;
            SubjectDescription = subjectDescription;
            Quizzes= new List<Quiz>();
            Lessons= new List<Lesson>();
        }
        public void AddQuizz(Quiz quiz)
        {
            Quizzes.Add(quiz);
        }

        public void AddLesson(Lesson lesson)
        {
            Lessons.Add(lesson);
        }

        public void ShowDetailsQuizzes()
        {
            Console.WriteLine($"Subject : {SubjectTitle} ({ShortForm})");
            Console.WriteLine("Quizz include");
            for (int i = 0; i < Quizzes.Count; i++)
            {
                Console.Write(i + 1 +". ");
                Quizzes[i].ShowDetails();
            }
        }

        public void ShowDetailsLessons()
        {
            Console.WriteLine($"Subject : {SubjectTitle} ({ShortForm})");
            Console.WriteLine("Lessons include");
            for (int i = 0; i < Lessons.Count; i++)
            {
                Console.Write(i + 1 + ". ");
                Lessons[i].ShowDetails();
            }
        }


        public override string ToString()
        {
            return" is the high score holder for " + SubjectTitle +
                " with score ";
        }



    }
}
