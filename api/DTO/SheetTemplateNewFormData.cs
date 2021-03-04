using System.Collections.Generic;

namespace wiseguy {
    public class SheetTemplateNewFormData {
        public string Course {get;set;}
        public string Subject {get;set;}
        public IList<string> Phrases {get;set;}
        public SheetTemplate GetSheetTemplate() {

            List<Phrase> phrases = new List<Phrase>();
            foreach (string text in Phrases) {
                if (string.IsNullOrWhiteSpace(text)) continue;

                Phrase phrase = new Phrase();
                phrase.Text = text;
                phrases.Add(phrase);
            }
            if (string.IsNullOrWhiteSpace(Course)
                || string.IsNullOrWhiteSpace(Subject)
                || phrases.Count == 0) {
                return null;
            }

            return new SheetTemplate {
                Course = Course,
                Subject = Subject,
                Phrases = phrases
            };
        }
    }

    
}