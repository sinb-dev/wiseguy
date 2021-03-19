using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace wiseguy
{
    public class SheetCopy
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public SheetTemplate SheetTemplate { get; set; }
        public string Course {get;set;}
        public string Subject {get;set;}
        public IList<Answer> Answers { get; set; } = new List<Answer>();
        public string Token { get; set; }
        public SheetIssue Issue { get; set; }
        public DateTime Completed { get; set; }

        public SheetCopy()
        {
            Token = WiseGuyUtils.CreateToken();
        }
        public static SheetCopy CreateFromTemplate(SheetTemplate template) {
            var copy = new SheetCopy();
            copy.Course = template.Course;
            copy.Subject = template.Subject;
            copy.SheetTemplate = template;
            copy.Token = WiseGuyUtils.CreateToken();
            //copy.Issue = 
            return copy;
        }
    }
}