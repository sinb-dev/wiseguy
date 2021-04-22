using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace wiseguy 
{
    public class IssuedCopiesDTO 
    {
        public int issueId {get;set;}
        public List<copy> copies {get;set;}
        public DateTime issueDate {get;set;}
        public string course {get;set;}
        public string subject {get;set;}

        public class copy {
            public string name  {get;set;}
            public string email  {get;set;}
            public string token {get;set;}
            public DateTime completed {get;set;}
            public int[] otherIssues {get;set;}
        }

        public static IssuedCopiesDTO Construct(SheetIssue issue) {
            IssuedCopiesDTO dto = new IssuedCopiesDTO();

            dto.course = issue.SheetTemplate.Course;
            dto.subject = issue.SheetTemplate.Subject;
            dto.issueId = issue.Id;
            dto.issueDate = issue.Issued;

            ///Maillist list = issue.Maillist;
            using (var context = new WiseGuyContext()) 
            {
                var list = context.Issues
                    .Include(i=>i.Copies)
                    .Include(c=>c.SheetTemplate)
                    .First(i=>issue.Id == i.Id).Copies;

                dto.copies = new List<copy>();
                foreach (var copy in list) {
                    //otherAnswers is a database query per copy! Terrible practice
                    var otherIssues = context.Copies
                        .Include(c=>c.Issue)
                        .Where(c => 
                            c.SheetTemplate.Equals(issue.SheetTemplate) 
                            && c.Email == copy.Email 
                            && !c.Equals(copy))
                        .Select(c => c.Issue.Id)
                        .ToArray();

                    dto.copies.Add( new copy { name=copy.Name, email=copy.Email, token=copy.Token, completed = copy.Completed, otherIssues = otherIssues});
                }
            }
            return dto;
        }

    }

}