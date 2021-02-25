using System;
using System.Collections.Generic;
namespace wiseguy
{
    public class p {
        public int id {get;set;}
        public string text {get;set;}
        public static p Construct(int i, string t) {
            return new p {
                id = i,
                text = t
            };
        }
    }
    public class Response
    {
        public string course {get;set;} = "Programmeringsmetodik";
        public string subject {get;set;} = "SCRUM";
        public List<p> phrases {get;set;} = new List<p> {
            p.Construct(1,"Product Owner"),
            p.Construct(2,"SCRUM Master"),
            p.Construct(3,"Product Backlog")
        };
    }
}