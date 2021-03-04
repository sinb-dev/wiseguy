using System.Collections.Generic;

namespace wiseguy {
    public class TemplateDTO 
    {
        public int id {get;set;}
        public string course {get;set;}
        public string subject {get;set;}
        public List<string> phrases {get;set;}
        public static TemplateDTO Construct(SheetTemplate t) {
            var dto = new TemplateDTO();
            dto.id = t.Id;
            dto.course = t.Course;
            dto.subject = t.Subject;
            dto.phrases = new List<string>();
            foreach (var p in t.Phrases) {
                dto.phrases.Add(p.Text);
            }
            return dto;
        }
    }
}