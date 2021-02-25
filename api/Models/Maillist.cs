using System.Collections.Generic;

namespace wiseguy {
    public class Maillist {
        public int Id {get;set;}
        public string Name {get;set;}
        public IList<Participant> Participants {get;set;} = new List<Participant>();
    }
}