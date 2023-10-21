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
        public User(string username, string password, string email,string gender) {
        
            Username = username;
            _password = password;
            Email = email;
            Gender = gender;
        }

        public virtual bool EditPassword(string oldPassword,string password)
        {
            if (_password.Equals(oldPassword) && password != "")
            {
                SetPassword(password);
                return true;
            }
            else
                return false;
            
        }

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
            return "User " + Username +" with Email " + Email;
        }
    }
}
