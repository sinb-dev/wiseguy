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
    [Route("list")]
    public class MaillistController : ControllerBase
    {
        private void bindEmailsToList(Maillist maillist, List<string> emails) {

        }

        [HttpPost("new")]
        public async Task<ActionResult<String>> NewMaillist([FromForm] MaillistNewDTO listdata)
        {
            
            using(var context = new WiseGuyContext()) {
                Maillist list = listdata.GetMaillist();

                if (list != null) {
                    context.Maillists.Add( list );
                    await context.SaveChangesAsync();
                    await this.AddParticipants(list.Id, listdata.emails);
                    var response = new Dictionary<string,int> {
                        {"id", list.Id}
                    };
                    return Ok(response);
                }
            }
            return Problem("Invalid request data");
        }

        [HttpPut("update")]
        public async Task<ActionResult<String>> UpdateMaillist([FromForm] MaillistUpdateDTO listdata)
        {
            if (listdata.id == 0) {
                return Problem("Error in data. Id must not be 0");
            }
            if (listdata == null) {
                return Problem("Error in data");
            }

            using(var context = new WiseGuyContext()) {
                Maillist list = null;

                list = context.Maillists.Include(m => m.Participants).First(m => m.Id == listdata.id);

                if (list == null)
                    return Problem("Invalid request data");

                list.Name = listdata.name;
                
                //If we received a new list of emails remove the old and use the new
                if (listdata.emails != null) {
                    list.Participants.Clear();
                    await context.SaveChangesAsync();

                    await this.AddParticipants(list.Id, listdata.emails);   
                }
                await context.SaveChangesAsync();
                return Ok("OK");
            }
            
        }

        [HttpGet("{mailListId}")]
        public ActionResult<String> GetTemplateData(int mailListId)
        {
            using(var context = new WiseGuyContext()) {
                Maillist maillist = null;
                try {
                    maillist = context.Maillists.Include(maillist=>maillist.Participants).First(m => m.Id == mailListId);
                } catch {}

                if (maillist == null)
                    return Problem("Unknown template");
                
                return Ok(MaillistUpdateDTO.Construct(maillist));
            }
        }
        
        [HttpGet("lists")]
        public ActionResult<String> GetLists()
        {
            
            using(var context = new WiseGuyContext()) {
                MaillistNameAndEmailCount dto = new MaillistNameAndEmailCount();

                try {
                    var lists = context.Maillists.Include(list=>list.Participants);
                    foreach (var l in lists) {
                        dto.Add(l.Id, l.Name, l.Participants.Count);
                    }
                } catch {}

                return Ok(dto.GetList());
            }
        }

        [HttpPut("{mailListId}/participant/add")]
        public async Task<ActionResult<String>> AddParticipants(int mailListId, [FromBody] IList<string> emails)
        {
            using(var context = new WiseGuyContext()) {
                var maillist = context.Maillists.Include(maillist=>maillist.Participants).First(m => m.Id == mailListId);
                
                if (maillist != null) 
                {
                    ParticipantController.CreateParticipantsFromEmails(emails);
                    //Find all the emails that is not created yet.
                    //List<string> nonexisting = new List<string>();

                    var existing = context.Participants.Where(p=>emails.Contains(p.Email));
                    /*foreach (string e in emails) {
                        bool found = false;
                        foreach (var p in existing) {
                            if ( p.Email == e ) {
                                found = true;
                                break;
                            }
                        }
                        if (!found) {
                            nonexisting.Add(e);
                        }
                    }

                    //Create participants from non existing mails
                    foreach (string email in nonexisting) {
                        Participant p = new Participant {
                            Email = email,
                            Name = email.IndexOf("@") >= 0? email.Substring(0, email.IndexOf("@")) : email,
                        };
                        maillist.Participants.Add(p);
                    }*/
                    
                    //Add existing emails to the maillist (if they weren't there already)
                    foreach (var p in existing) {
                        if (maillist.Participants.Contains(p) == false)
                            maillist.Participants.Add(p);
                    }
                    
                    await context.SaveChangesAsync();
                    return Ok("OK");
                }
            }
            return Problem("Invalid request data");
        }

        [HttpDelete("{mailListId}/participant/remove")]
        public async Task<ActionResult<String>> RemoveParticipants(int mailListId, [FromForm] List<string> emails) 
        {
            List<Participant> toBeRemoved = new List<Participant>();
            
            using (var context = new WiseGuyContext()) 
            {
                var maillist = context.Maillists.Include(maillist=>maillist.Participants).First(m => m.Id == mailListId);

                var existing = context.Participants.Where(p=>emails.Contains(p.Email));
                foreach(var p in existing) {
                    maillist.Participants.Remove(p);
                }

                await context.SaveChangesAsync();
                return Ok("OK");
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<String>> DeleteMailList([FromBody] int id)
        {
            using(var context = new WiseGuyContext()) {
                var maillist = context.Maillists.First(m => m.Id == id);
                if (maillist != null) 
                {
                    context.Maillists.Remove(maillist);
                    await context.SaveChangesAsync();
                    return Ok("OK");
                }
            }
            return Problem("Not found");
        }
    }
}