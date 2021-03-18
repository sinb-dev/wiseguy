
using System.Collections.Generic;
namespace wiseguy {
    public class EvaluationAnswerDTO 
    {
        public class answerbuffer {
            public int id {get;set;}
            public AnswerType answer {get;set;}
            public string text {get;set;}
        }
        public string sender {get;set;}
        public List<answerbuffer> answers {get;set;}

        public List<Answer> GetAnswers() 
        {
            var answers = new List<Answer>();

            foreach (var a in this.answers) {
                answers.Add(new Answer {
                    AnswerId = a.id,
                    Sheet = null,
                    Phrase = a.text,
                    AnswerType = a.answer
                });
            }

            return answers;
        }
    }
}