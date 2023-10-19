using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningPlatform
{
    internal class SubjectQuizz
    {
        public string SubjectTitle { get; set; }
        public string SubjectDescription { get; set; }

        public double HighScore { get; set; }
        public Student HighScoreHolder { get; set; }
        public Dictionary<string, Question> QuestionList { get; set; }
        //public Teacher HighScoreHolderTeacher { get; set; }

        public SubjectQuizz(string subjectTitle, string subjectDescription)
        {
            SubjectTitle = subjectTitle;
            SubjectDescription = subjectDescription;
            QuestionList = new Dictionary<string,Question>();
            HighScore = 0;
        }
        public void addQuestion(Question question)
        {
            QuestionList.Add(question.Title, question);
        }

        public override string ToString()
        {
            return HighScoreHolder + " is the high score holder for " + SubjectTitle +
                " with score " + HighScore;
        }



    }
}
