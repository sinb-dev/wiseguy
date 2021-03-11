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

        public class copy {
            public string name  {get;set;}
            public string email  {get;set;}
            public string token {get;set;}
        }

        public static IssuedCopiesDTO Construct(SheetIssue issue) {
            IssuedCopiesDTO dto = new IssuedCopiesDTO();

            dto.issueId = issue.Id;
            dto.issueDate = issue.Issued;

            ///Maillist list = issue.Maillist;
            using (var context = new WiseGuyContext()) 
            {
                var list = context.Issues.Include(i=>i.Copies).First(i=>issue.Id == i.Id).Copies;
           
                dto.copies = new List<copy>();
                foreach (var copy in list) {
                    dto.copies.Add( new copy { name=copy.Name, email=copy.Email, token=copy.Token });
                }
             }
            return dto;
        }

    }

}