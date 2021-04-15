using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace wiseguy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParticipantController : ControllerBase
    {
        [HttpPost("new")]
        public async Task<ActionResult<String>> NewParticipant([FromForm] NewParticipantFormData p)
        {
            using(var context = new WiseGuyContext()) {
                if (p != null) {
                    
                    var participant = p.GetParticipant();
                    
                    //Attach to maillist if set
                    foreach (var mid in p.Maillists) {
                        var maillist = context.Maillists.First(m=>m.Id == mid);
                        
                        if (maillist != null) {
                            maillist.Participants.Add(participant);
                        }
                    }
                    
                    context.Participants.Add( participant );
                    await context.SaveChangesAsync();
                    return Ok("OK");
                }
            }
            return Problem("Incorrect template data");
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<String>> DeleteParticipant([FromBody] Participant p)
        {
            using(var context = new WiseGuyContext()) {
                if (p != null) {
                    context.Participants.Add( p );
                    await context.SaveChangesAsync();
                    return Ok("OK");
                }
            }
            return Problem("Incorrect template data");
        }

        public static IList<Participant> CreateParticipantsFromEmails(IList<string> emails) 
        {
            IList<Participant> list = new List<Participant>();
            using (var context = new WiseGuyContext())
            {
                //Find all the emails that is not created yet.
                List<string> nonexisting = new List<string>();
                var existing = context.Participants.Where(p=>emails.Contains(p.Email));
                foreach (string e in emails) {
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
                        AccessToken = WiseGuyUtils.CreateToken(),
                        Name = email.IndexOf("@") >= 0? email.Substring(0, email.IndexOf("@")) : email,
                    };
                    context.Participants.Add(p);
                    list.Add(p);
                }
                context.SaveChanges();
            }
            return list;
        }
    }
}