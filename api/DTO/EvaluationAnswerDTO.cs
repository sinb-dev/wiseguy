
using System.Collections.Generic;
using System.Linq;
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

            var ids = this.answers.Select( a => a.id).ToList();

            
            using (var context = new WiseGuyContext()) {
                var phrases = context.Phrases.Where(p => ids.Contains(p.Id)).ToDictionary(p => p.Id);

                foreach (var a in this.answers) {
                    if (!phrases.ContainsKey(a.id)) continue;
                    
                    var ans = new Answer {
                        AnswerId = a.id,
                        Sheet = null,
                        AnswerType = a.answer
                    };
                    answers.Add(ans);
                }
            }


            

            return answers;
        }
    }
}