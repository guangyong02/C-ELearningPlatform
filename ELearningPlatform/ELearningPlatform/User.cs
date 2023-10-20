using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ELearningPlatform
{
    internal class User
    {
        public string Username { get; set; }
        private string _password;
        public string Email { get; set; }
        public string Gender { get; }
        private void SetPassword(string password)
        {
            _password = password;
        }

        //Todo Remove
        public User(string username, string password, string email,string gender) {
        
            this.Username = username;
            _password = password;
            this.Email = email;
            Gender = gender;
        }

        public bool EditPassword(string oldPassword,string password)
        {
            if (_password.Equals(oldPassword) && password != "")
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
        //Todo Remove password 
        public bool CheckPasswor(string password)
        {
            return _password.Equals(password);
        }
        public virtual void ShowDetails()
        {
            Console.WriteLine("Username\t: "+Username);
            Console.WriteLine("Email\t\t: "+Email);
        }
        public override string ToString()
        {
            //return "User " + UserName + " with Email " + Email;
            return "User " + Username +" Password "+_password+ " with Email " + Email;
        }
    }
}
