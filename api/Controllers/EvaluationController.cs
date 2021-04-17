using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace wiseguy.Controllers
{
    [ApiController]
    [Route("eval")]
    public class EvaluationController : ControllerBase
    {
        class form {
            public string course {get;set;}
            public string subject {get;set;}
            public DateTime completed {get;set;}
            public List<Phrase> phrases {get;set;} = new List<Phrase>();
            public Dictionary<string,List<formAnswer>> answers {get;set;} = new Dictionary<string,List<formAnswer>>();
        }
        class formAnswer {
            public AnswerType answer {get;set;}
            public int issueId {get;set;}
            public DateTime completed {get;set;}
        }
    

        public EvaluationController(ILogger<EvaluationController> logger)
        {
        }

        [HttpGet("{copyToken}")]
        public async Task<ActionResult<SheetCopy>> Get(string copyToken)
        {
            using (var context = new WiseGuyContext()) 
            {
                SheetCopy copy = null;
                try {
                    copy = context.Copies
                        .Include(c=>c.SheetTemplate)
                        .Include(c=>c.Issue)
                        .Include(c=>c.SheetTemplate.Phrases)
                        .Include(c=>c.Answers)
                        .First(c => c.Token == copyToken);
                } catch {}

                if (copy == null)
                    return Problem("No such copy");

                var form = new form();
                form.course = copy.Course;
                form.subject = copy.Subject;
                form.completed = copy.Completed;

                foreach (var p in copy.SheetTemplate.Phrases) {
                    var tmp = p;
                    tmp.SheetTemplate=null;
                    form.phrases.Add(p);
                }

                foreach (var p in copy.Answers) {
                   // form.answers.Add(p.Phrase.Id.ToString(), new formAnswer() { answer = p.AnswerType, issueId = copy.Issue.Id, completed = copy.Completed});
                }

                try {
                    var alltimeAnswersForTemplate = context.Answers
                        .Include(c=>c.Sheet)
                        .Include(c=>c.Phrase)
                        .Include(c=>c.Sheet.SheetTemplate)
                        .Where(c => c.Sheet.Email == copy.Email && c.Sheet.SheetTemplate.Equals(copy.SheetTemplate))
                        .ToList();
                    foreach (var a in alltimeAnswersForTemplate) {
                        string pid = a.Phrase.Id.ToString();
                        var answer = new formAnswer() { answer = a.AnswerType, issueId = copy.Issue.Id, completed = a.Sheet.Completed};
                        if (!form.answers.ContainsKey(pid))
                        {
                            form.answers[pid] = new List<formAnswer>();
                        }

                        form.answers[pid].Add(answer);
                    }   
                } catch {

                }

                return Ok(form);
            }
        }

        [HttpPost("{copyToken}")]
        public async Task<ActionResult<SheetCopy>> Get(string copyToken, EvaluationAnswerDTO evaluation)
        {
            using (var context = new WiseGuyContext()) 
            {
                var copy = context.Copies
                    .Include(c=>c.SheetTemplate)
                    .Include(c=>c.SheetTemplate.Phrases)
                    .First(c => c.Token == copyToken);

                if (copy.Completed != DateTime.MinValue) {
                    return Problem("This copy has already been completed");
                }
                
                var phraseIds = evaluation.answers.Select(a => a.id).ToList();
                var phrases = context.Phrases
                    .Where(p => phraseIds.Contains(p.Id))
                    .ToDictionary(p => p.Id);

                foreach (var answerDTO in evaluation.answers) 
                {
                    var a = new Answer {
                        AnswerId = 0,
                        Sheet = copy,
                        AnswerType = answerDTO.answer,
                        Phrase = phrases[answerDTO.id]
                    };

                    context.Answers.Add(a);
                }
                copy.Completed = DateTime.Now;
                await context.SaveChangesAsync();

                return Ok("OK");
            }
        }
    }
}
