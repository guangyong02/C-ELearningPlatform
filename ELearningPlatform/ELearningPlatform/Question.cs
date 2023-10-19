using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningPlatform
{
    internal class Question
    {
        public string Topic { get; set; }
        public string Answer { get; set; }
        public string Explaination { get; set; }

        public Question(string title,string answer, string explaination)
        {
            Topic = title;
            Answer = answer;
            Explaination = explaination;
        }

        public override string ToString()
        {
            return "The answer for "+ Topic + " is " + Answer + 
                "\nIt is because " +Explaination;
        }





    }
}
