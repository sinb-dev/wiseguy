using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace wiseguy {
    public enum AnswerType { Unknown, NeverHeard, HeardBefore, CanExplain };
    
    public class Answer 
    {
        [Key]
        public int AnswerId {get;set;}
        public SheetCopy Sheet {get;set;}
        public string Phrase {get;set;}
        public AnswerType AnswerType {get;set;}
    }
}