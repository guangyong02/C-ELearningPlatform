using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningPlatform
{
    internal class Student : User
    {
        public double StudyFees { get; }

        public Dictionary<string,double> QuizTaken { get; }

        public List<string> LessonTaken { get; }
        public Student(string username, string password, string email, double studyFees, string gender) : base(username, password, email, gender)
        {
            StudyFees = studyFees;
            QuizTaken= new Dictionary<string,double>();
            LessonTaken = new List<string>();
        }

        public void FinishAQuiz(string quizTitle, double marks)
        {
            QuizTaken[quizTitle] = marks;
        }

        public void FinishALesson(string lessonTitle)
        {
            LessonTaken.Add(lessonTitle);
        }

        public override void ShowDetails()
        {
            Console.WriteLine("User Type\t: Student");
            base.ShowDetails();
            if (QuizTaken.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Have Taken Quizzes include\t:");
                int count = 0;
                foreach (KeyValuePair<string,double> quizTaken in QuizTaken)
                {
                    Console.WriteLine($"{++count}. {quizTaken.Key,-15}" + "\t\t: "+Math.Round(quizTaken.Value,2)+"%");
                }
            }
            if (LessonTaken.Count > 0)
            {
                Console.WriteLine();
                int count = 0;
                Console.WriteLine("Have Taken Lessons include\t:");
                foreach (string lesson in LessonTaken)
                {
                    Console.WriteLine($"{++count}.{lesson}");
                }
            }
            

        }

        public override string ToString()
        {
            return "Student " + base.ToString() + " StudyFees " + StudyFees;
        }
    }
}
