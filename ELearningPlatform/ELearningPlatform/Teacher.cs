using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningPlatform
{
    internal class Teacher : User
    {
        public double Salary { get; set; }
        public Teacher(string UserName, string Password, string Email, double salary) : base(UserName, Password, Email)
        {
            Salary = salary;
        }

        public override string ToString()
        {
            return "Teacher " + base.ToString() + " Salary " + Salary;
        }

    }
}
