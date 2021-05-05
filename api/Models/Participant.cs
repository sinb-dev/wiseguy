using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
namespace wiseguy 
{
    public class Participant {
        [Key]
        public string Email {get;set;}
        public string Name {get;set;}
        public string AccessToken {get;set;}
        public IList<Maillist> Maillists {get;set;} = new List<Maillist>();
        public DateTime Created {get;set;}

        public static List<Participant> GetParticipantsFromEmails(List<string> emails) 
        {
            List<Participant> list = new List<Participant>();

            using (var context = new WiseGuyContext()) 
            {
                return context.Participants.Where( p => emails.Contains(p.Email)).ToList();
            }
        }
    }
}