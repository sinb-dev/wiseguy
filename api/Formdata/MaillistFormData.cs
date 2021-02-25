using System.Collections.Generic;
namespace wiseguy {
    public class MaillistNewFormData 
    {
        
        public string Name {get;set;}
        public IList<string> Emails {get;set;}
        public Maillist GetMaillist() {
            return new Maillist {
                Name = Name
            };
        }
    }
    public class MaillistUpdateFormData : MaillistNewFormData
    {
        public int Id {get;set;}
        public new Maillist GetMaillist() {
            var list = base.GetMaillist();
            list.Id = Id;
            return list;
        }
    }
}