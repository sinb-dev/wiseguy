using System;
using System.ComponentModel.DataAnnotations;
namespace wiseguy {
    public class User 
    {
        [Key]
        public int UserId {get;set;}
        public string Username {get;set;}
        public string Password {get;set;}
        public string Email {get;set;}
        public string Session {get;set;}
        public DateTime LastActivity {get;set;}
        public DateTime Created {get;set;}
    }
}