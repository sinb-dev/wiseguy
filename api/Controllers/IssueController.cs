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
    [Route("issue")]
    public class IssueController : ControllerBase
    {
        public IssueController(ILogger<EvaluationController> logger)
        {
        }

        [HttpGet("{issueId}/copies")]
        public ActionResult<String> GetIssuedCopies(int issueId)
        {
            using(var context = new WiseGuyContext()) {
                SheetIssue issue = null;
                try {
                    issue = context.Issues.Include(i=>i.Maillist).Include(i=>i.Maillist.Participants).First(i => i.Id == issueId);
                } catch {}

                if (issue == null)
                    return Problem("Unknown issue");
                
                return Ok(IssuedCopiesDTO.Construct(issue));
            }
        }
        

        [HttpPost("{templateId}/issue")]
        public async Task<ActionResult<IssuedCopiesDTO>> IssueTemplateForEmails(int templateId, [FromForm] int maillistId)
        {
            using(var context = new WiseGuyContext()) {
                SheetTemplate template = null;
                try {
                    template = context.Templates.First(t => t.Id == templateId);
                } catch {}
                if (template == null)
                    return Problem("Unknown template");
                
                var maillist = context.Maillists.Include(m => m.Participants).First(m => m.Id == maillistId);
                if (maillist == null) 
                    return Problem("Unknown mail list");

                SheetIssue issue = SheetIssue.Issue(maillist, template);
                if (issue != null) 
                {
                    context.Issues.Add(issue);
                    await context.SaveChangesAsync();
                    return Ok(IssuedCopiesDTO.Construct(issue));
                }
                
                return Ok(null);
            }
        }
    }
}
