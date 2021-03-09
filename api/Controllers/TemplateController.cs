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
    [Route("tpl")]
    public class TemplateController : ControllerBase
    {
        public TemplateController(ILogger<EvaluationController> logger)
        {
        }

        [HttpPost("new")]
        public async Task<ActionResult<String>> NewSheetTemplate([FromForm] SheetTemplateNewFormData data)
        {
            using(var context = new WiseGuyContext()) {
                var o = data.GetSheetTemplate();
                if (o != null) {
                    context.Templates.Add( o );
                    await context.SaveChangesAsync();
                    return Ok("OK");
                }
            }
            return Problem("Incorrect template data");  
        }

        [HttpPost("update")]
        public async Task<ActionResult<String>> UpdateSheetTemplate([FromForm] SheetTemplateUpdateFormData data)
        {
            using(var context = new WiseGuyContext()) {
                var tpl = context.Templates.Include(tpl=>tpl.Phrases).First(tpl => tpl.Id == data.Id);
                tpl.Course = data.Course;
                tpl.Subject = data.Subject;

                tpl.Phrases = data.GetSheetTemplate().Phrases;
                
                await context.SaveChangesAsync();
                return Ok("OK");
            }
        }

        [HttpPost("{templateId}/copy")]
        public async Task<ActionResult<String>> CopyTemplateForEmails(int templateId, [FromForm] string[] emails)
        {
            using(var context = new WiseGuyContext()) {
                var template = context.Templates.First(t => t.Id == templateId);
                if (template == null)
                    return Problem("Unknown template");
                
                var issue = SheetIssue.Issue(new List<string>(emails), template);
                context.Issues.Add(issue);
                await context.SaveChangesAsync();
                return Ok("OK");
            }
        }
        [HttpGet("{templateId}")]
        public ActionResult<String> GetTemplateData(int templateId)
        {
            using(var context = new WiseGuyContext()) {
                SheetTemplate template = null;
                try {
                    template = context.Templates.Include(tpl=>tpl.Phrases).First(t => t.Id == templateId);
                } catch {}

                if (template == null)
                    return Problem("Unknown template");
                
                return Ok(TemplateDTO.Construct(template));
            }
        }
        

        [HttpPost("{templateId}/issue")]
        public async Task<ActionResult<String>> IssueTemplateForEmails(int templateId, [FromForm] int maillistId)
        {
            using(var context = new WiseGuyContext()) {
                var template = context.Templates.First(t => t.Id == templateId);
                if (template == null)
                    return Problem("Unknown template");
                
                var maillist = context.Maillists.First(m => m.Id == maillistId);
                if (maillist == null) 
                    return Problem("Unknown mail list");

                SheetIssue issue = SheetIssue.Issue(maillist, template);
                context.Issues.Add(issue);
                await context.SaveChangesAsync();
                return Ok("OK");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<String>> DeleteTemplate(int id)
        {
            using(var context = new WiseGuyContext()) 
            {
                var template = context.Templates.First(a => a.Id == id);
                if (template != null) {
                    context.Templates.Remove(template);
                    await context.SaveChangesAsync();
                    return Ok("OK");
                } 
                return Problem("Template not found");
            }
        }

        [HttpGet("phrases/{id}")]
        public async Task<ActionResult<Response>> GetPhrases(int id) 
        {
            return Ok(new Response());
        }
    }
}
