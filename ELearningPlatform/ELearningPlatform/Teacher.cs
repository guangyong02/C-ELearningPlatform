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
        public Teacher(string username, string password, string email, double salary,string gender) : base(username, password, email,gender )
        {
            Salary = salary;
        }

        public override string ToString()
        {
            //return "Teacher " + base.ToString() + " Salary " + Salary;
            return "Teacher " +base.Username;
        }

    }
}
