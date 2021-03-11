using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace wiseguy {
    public class SheetIssue 
    {
        [Key]
        public int Id {get;set;}
        public SheetTemplate SheetTemplate { get; set; }
        public List<SheetCopy> Copies { get; set; }
        public DateTime Issued {get;set;}
        public Maillist Maillist {get;set;}
        
        public static SheetIssue Issue(Maillist list, SheetTemplate template) 
        {
            using (var context = new WiseGuyContext()) {
                var issue = new SheetIssue();
                issue.SheetTemplate = template;
                issue.Issued = DateTime.Now;
                issue.Maillist = list;
                issue.Copies = issue.createCopies(list.Participants, template);
                foreach (var c in issue.Copies) {
                    context.Copies.Add(c);
                }
                context.Issues.Add(issue);
                context.SaveChangesAsync();
                return issue;
            }
        }
        public static SheetIssue Issue(List<string> emails, SheetTemplate template)
        {
            using (var context = new WiseGuyContext()) {
                var issue = new SheetIssue();
                issue.SheetTemplate = template;
                issue.Issued = DateTime.Now;
                var list = Controllers.ParticipantController.CreateParticipantsFromEmails(emails);
                issue.Copies = issue.createCopies(list, template);
                foreach (var c in issue.Copies) {
                    context.Copies.Add(c);
                }
                context.Issues.Add(issue);
                context.SaveChangesAsync();
                return issue;
            }
        }

        private List<SheetCopy> createCopies(IList<Participant> participants, SheetTemplate template) 
        {
            List<SheetCopy> copies = new List<SheetCopy>();
            foreach (var p in participants) {

                var copy = SheetCopy.CreateFromTemplate(template);
                copy.Email = p.Email;
                copy.Issue = this;
                copy.Name = p.Name;
                copies.Add(copy);
            }
            return copies;
        }
    }
}