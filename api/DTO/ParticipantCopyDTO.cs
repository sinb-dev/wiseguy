using System;
using System.Collections.Generic;
namespace wiseguy
{
    public class ParticipantCopyDTO 
    {
        public string name {get;set;}
        public class row {
            public string subject {get;set;}
            public string course {get;set;}
            public DateTime date {get;set;}
            public DateTime completed {get;set;}
            public string token {get;set;}
        }
        public List<row> copies {get;set; }= new List<row>();
        public void Add(string subject, string course, DateTime date, DateTime completed, string token) {
            copies.Add( new row{ subject=subject, course=course, date=date, completed=completed, token=token });
        }
    }
}