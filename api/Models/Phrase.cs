
using System.ComponentModel.DataAnnotations;
namespace wiseguy {
    public class Phrase {
        [Key]
        public int Id {get;set;}
        public string Text {get;set;}
        public SheetTemplate SheetTemplate {get;set;}
        /*public static Phrase Construct(string text, SheetTemplate template) 
        {
            return new 
        }*/
    }
}