using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningPlatform
{
    internal class User
    {
        public string UserName { get; set; }
        private string _password;
        public string Email { get; set; }
        public string Gender { get; }
        private void SetPassword(string password)
        {
            _password = password;
        }
        public User(string UserName, string Password, string Email,string gender) {
        
            this.UserName = UserName;
            _password = Password;
            this.Email = Email;
            Gender = gender;
        }

        public bool EditPassword(string email,string password)
        {
            if (Email.Equals(email) && password != "")
            {
                SetPassword(password);
                return true;
            }
            else
                return false;
            
        }
        //Todo add a method for change password using oldpassword 
        //public bool editPassword(string oldPassword, string password)
        //{
        //    if (_password.Equals(oldPassword) && password != "")
        //    {
        //        setPassword(password);
        //        return true;
        //    }
        //    else
        //        return false;

        //}

        public override string ToString()
        {
            //return "User " + UserName + " with Email " + Email;
            return "User " + UserName +" Password "+_password+ " with Email " + Email;
        }
    }
}
