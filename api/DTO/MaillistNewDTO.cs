using System.Collections.Generic;
namespace wiseguy {
    public class MaillistNewDTO
    {
        public string name {get;set;}
        public IList<string> emails {get;set;}
        public Maillist GetMaillist() {
            var list = new Maillist();
            list.Name = name;
            
            return list;
        }
        public MaillistNewDTO Construct(Maillist o) {
            var dto = new MaillistNewDTO();
            dto.name = o.Name;
            dto.emails = new List<string>();
            foreach (var p in o.Participants) {
                dto.emails.Add(p.Email);
            }
            return dto;
        }
    }
    public class MaillistUpdateDTO : MaillistNewDTO
    {
        public int id {get;set;}
        public new Maillist GetMaillist() {
            var list = base.GetMaillist();
            //list.id = Id;
            return list;
        }
        public static new MaillistUpdateDTO Construct(Maillist o) {
            var dto = new MaillistUpdateDTO();
            dto.name = o.Name;
            dto.id = o.Id;
            dto.emails = new List<string>();
            foreach (var p in o.Participants) {
                dto.emails.Add(p.Email);
            }
            return dto;
        }
    }
}