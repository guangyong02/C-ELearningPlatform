using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningPlatform
{
    internal class Student : User
    {
        public double studyFees;
        public Student(string username, string password, string email, double studyFees, string gender) : base(username, password, email, gender)
        {
            this.studyFees = studyFees;
        }

        public Student():base("","","","")
        {
            
        }

        public override string ToString()
        {
            return "Student " + base.ToString() + " StudyFees " + studyFees;
        }
    }
}
