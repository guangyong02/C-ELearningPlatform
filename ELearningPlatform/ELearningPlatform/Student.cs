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
        public Student(string UserName, string Password, string Email, double studyFees) : base(UserName, Password, Email)
        {
            this.studyFees = studyFees;
        }

        public override string ToString()
        {
            return "Student " + base.ToString() + " StudyFees " + studyFees;
        }
    }
}
