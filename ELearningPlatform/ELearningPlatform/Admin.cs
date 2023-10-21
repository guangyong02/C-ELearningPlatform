using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningPlatform
{
    internal class Admin : User
    {
        private string _backupEmail;
        public Admin(string username, string password, string email, string gender, string backupEmail) : base(username, password, email, gender)
        {
            _backupEmail = backupEmail;
        }

        public override void ShowDetails()
        {
            Console.WriteLine("User Type\t: Admin");
            base.ShowDetails();
        }
        //Todo Edit Password
        public bool EditPassword(string backupEmail, string oldPassword, string password)
        {
            if (_backupEmail.Equals(backupEmail))
            {
                return base.EditPassword(oldPassword, password);
            }
            else
                return false;
        }
    }
}
