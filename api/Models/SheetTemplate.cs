using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace wiseguy {
    public class SheetTemplate {
        [Key]
        public int Id {get;set;}
        public string Course {get;set;}
        public string Subject {get;set;}
        public DateTime Created {get;set;}
        public DateTime LastUsed {get;set;}
        public IList<Phrase> Phrases {get;set;}
        public IList<SheetCopy> Copies {get;set;}
    }
}