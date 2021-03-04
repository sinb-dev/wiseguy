using System.Collections.Generic;
using System.Linq;

namespace wiseguy {
    public class NewParticipantFormData 
    {
        public string Email {get;set;}
        public string Name {get;set;}
        public int[] Maillists {get;set;}
        public Participant GetParticipant() {
            return new Participant() {
                Email = Email,
                Name = Name
            };
        }
        public List<Maillist> GetMailLists() {
            var list = new List<Maillist>();
            using (var context = new WiseGuyContext()) {
                foreach (int id in Maillists)
                {
                    var maillist = context.Maillists.First(m => m.Id == id);
                    if (maillist != null) {
                        list.Add(maillist);
                    }
                }
            }
            return list;
        }
    }
}