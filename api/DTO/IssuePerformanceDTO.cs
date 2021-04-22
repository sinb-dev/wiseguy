using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace wiseguy 
{
    public class IssuePerformanceDTO 
    {
        
        public DateTime lastest_submission = DateTime.Now;
        public List<_phrase> phrases {get;set;} = new List<_phrase>();
        public int recipients {get;set;}

        public class copy {
            public string name  {get;set;}
            public string email  {get;set;}
            public string token {get;set;}
            public DateTime completed {get;set;}
            public int[] otherIssues {get;set;}
        }
        
        public class _phrase {
            public class issueAnswerCount {
                public int issueId {get;set;}
                public int count {get;set;}
                public issueAnswerCount(int issueId, int count) { this.issueId = issueId; this.count = count;}
            }
            public int id {get;set;}
            public string phrase {get;set;}
            public Dictionary<int,issueAnswerCount> answerHelper = new Dictionary<int, issueAnswerCount>(); 
            
            public Dictionary<string, List<issueAnswerCount>> answers {get; set;} = new Dictionary<string, List<issueAnswerCount>>();
            public _phrase(int id, string phrase) { this.id = id;this.phrase = phrase;
                foreach (int i in Enum.GetValues(typeof(AnswerType)))  
                {  
                    answers[i+""] = new List<issueAnswerCount>();
                    answers.Remove("0");
                }
            }
        }

        public static IssuePerformanceDTO Construct(SheetIssue issue) {
                /**Target sql query
                select i.id, p.[Text], a.AnswerType, i.MaillistId,i.SheetTemplateId  from issues i
                left join copies c on c.IssueId = i.Id 
                left join answers a on a.SheetId = c.id
                left join phrases p on a.PhraseId = p.id
                where i.MaillistId = 2015
                and i.SheetTemplateId = 2009*/

            IssuePerformanceDTO dto = new IssuePerformanceDTO();
            
            
            using (var context = new WiseGuyContext()) 
            {
                //Count amount of recipients
                dto.recipients = context.Maillists
                    .Include(m=>m.Participants)
                    .Where(m=>m.Equals(issue.Maillist)).FirstOrDefault().Participants.Count;

                //summen af svar på hvor issue
                var issues = context.Issues
                    .Include(i=>i.Copies)
                    .Where(i=>i.SheetTemplate == issue.SheetTemplate
                        && i.Maillist == issue.Maillist).ToList();
                var copies = new List<SheetCopy>();
                foreach (var i in issues) {
                    copies.AddRange(i.Copies);
                }
                var answers = context.Answers
                    .Include(a=>a.Phrase)
                    .Include(a=>a.Sheet)
                    .Include(a=>a.Sheet.Issue)
                    .Where(a=>copies.Contains(a.Sheet))
                    .ToList();
                var _phrases = new Dictionary<int,_phrase>();
                var counter = new Dictionary<string,int>();
                var phraseMap = new Dictionary<int,string>();
                var issueAndCounts = new Dictionary<string,_phrase.issueAnswerCount>();
                string answerTypeString = "";
                string hash = "";
                foreach (var r in answers) {
                    answerTypeString = ((int)r.AnswerType)+"";
                    hash = $"{r.Phrase.Id}-{answerTypeString}-{r.Sheet.Issue.Id}";
                    if (!counter.ContainsKey(hash)) {
                        counter[hash] = 0;
                    }
                    counter[hash]++;
                    phraseMap[r.Phrase.Id] = r.Phrase.Text;

                    if (!_phrases.ContainsKey(r.Phrase.Id)) 
                        _phrases[r.Phrase.Id] = new _phrase(r.Phrase.Id,r.Phrase.Text);
                }
                foreach (KeyValuePair<string,int> kv in counter) {
                    string[] keys = kv.Key.Split("-");
                    int phraseId = int.Parse(keys[0]);
                    string answerTypeIndex = keys[1];
                    int issueId = int.Parse(keys[2]);
                    var p = _phrases[phraseId];

                    p.answers[answerTypeIndex].Add( new _phrase.issueAnswerCount(issueId, counter[kv.Key]));
                }
                dto.phrases.AddRange(_phrases.Values);

                /*foreach (var r in answers) {
                    answerTypeString = ((int)r.AnswerType)+"";
                    hash = $"{r.Phrase.Text}-{answerTypeString}-{r.Sheet.Id}";

                    var p = _phrases[r.Phrase.Id];
                    
                    
                    bool found = false;
                    foreach (var issueNumberCount in p.answers[answerTypeString]) {
                        if (issueNumberCount.issueId == r.Sheet.Issue.Id) {
                            issueNumberCount.count++;
                            found = true;
                            break;
                        }
                    }
                    if (found == false)
                        p.answers[answerTypeString].Add(new _phrase.issueAnswerCount(r.Sheet.Issue.Id,1));
                    
                }
                foreach (var p in _phrases.Values) {        
                    dto.phrases.Add(p);
                }*/
                dto = dto;
                /*var issueAnswerCount = new Dictionary<int,int>();
                foreach (var r in answers) {
                    if (!issueAnswerCount.ContainsKey(r.Sheet.Issue.Id)) {
                        issueAnswerCount[r.Sheet.Issue.Id] = 0;
                    }
                    if (r)                    issueAnswerCount[r.Sheet.Issue.Id]++;
                }*/
            }
            return dto;
        }

    }

}