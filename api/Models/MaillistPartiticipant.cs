using System.ComponentModel.DataAnnotations;

namespace wiseguy 
{
    public class MaillistParticipant {
        [Key]
        public int MaillistId {get;set;}
        [Key]
        public int ParticipantId {get;set;}
        
        public Maillist Maillist {get; private set;}
        
        public Participant Participant {get; private set;}
    }
}