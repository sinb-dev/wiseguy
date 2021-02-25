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
        IList<Answer> Answers { get; set; } = new List<Answer>();
        public string Token { get; set; }
        public SheetIssue Issue { get; set; }
        public DateTime Completed { get; set; }

        public SheetCopy()
        {
            Token = CreateToken();
        }
        public static string CreateToken(int length = 32)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        public static SheetCopy CreateFromTemplate(SheetTemplate template) {
            var copy = new SheetCopy();
            copy.Course = template.Course;
            copy.Subject = template.Subject;
            copy.SheetTemplate = template;
            copy.Token = CreateToken();
            //copy.Issue = 
            return copy;
        }
    }
}