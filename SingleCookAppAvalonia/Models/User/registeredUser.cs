using System;
using System.Collections.Generic;

//Might need to modify it later, also need to implement singleton class for RegisteredUserManager 
namespace SingleCookAppAvalonia.Models
{
    public class registeredUser
    {
        public string Username { get; set; }

        //public string Email {get; set;}
        public string Password { get; set; }

        public registeredUser(string u, string p){
            Username = u;
            //Email = e;
            Password = p;
        }

    }
}
