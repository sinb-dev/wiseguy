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

        [HttpGet("templates/{token}")]
        public async Task<ICollection<SheetTemplate>> getTemplatesFromParticipantToken(string token) 
        {
            List<SheetTemplate> list = new List<SheetTemplate>();
            using (var context = new WiseGuyContext()) 
            {
                try {
                    string email = context.Participants.First(p=>p.AccessToken == token).Email;

                    list = context.Copies
                        .Include(c => c.SheetTemplate)
                        .Where(c => c.Email == email)
                        .Select(c => c.SheetTemplate).Distinct().ToList();
                } catch {

                }
            }
            
            return list; 
        }
        [HttpGet("copies/{token}")]
        public async Task<ParticipantCopyDTO> getCopiesFromParticipantToken(string token) 
        {
            ParticipantCopyDTO dto = new ParticipantCopyDTO();
            
            using (var context = new WiseGuyContext()) 
            {
                try {
                    Participant p = context.Participants.First(p=>p.AccessToken == token);
                    dto.name = p.Name;
                    var copies = context.Copies
                        .Include(c => c.Issue)
                        .Include(c => c.SheetTemplate)
                        .Where(c => c.Email == p.Email)
                        .OrderByDescending(c => c.Issue.Issued)
                        .ToList();

                    foreach (var copy in copies) {
                        dto.Add(copy.SheetTemplate.Subject, copy.SheetTemplate.Course, copy.Issue.Issued, copy.Completed, copy.Token);
                    }
                } catch {

                }
            }
            
            return dto; 
        }
        public static IList<Participant> CreateParticipantsFromEmails(IList<string> emails) 
        {
            IList<Participant> list = new List<Participant>();
            using (var context = new WiseGuyContext())
            {
                //Find all the emails that is not created yet.
                Dictionary<string,string> nonexisting = new Dictionary<string, string>();
                var existing = context.Participants.Where(p=>emails.Contains(p.Email));
                foreach (string row in emails) {
                    var bits = row.Trim().Split(' ');
                    var email = "";
                    var name = "";
                    if (bits.Length == 1)
                        email = bits[0];
                    else if(bits.Length == 2) {
                        email = bits[1];
                        name = bits[0];
                    }
                    email = email.Trim();

                    if (name == "") {
                        name = email.IndexOf("@") >= 0? email.Substring(0, email.IndexOf("@")) : email;
                    }

                    Participant result = context.Participants.Where(p=> p.Email==email).FirstOrDefault();

                    if (result == null) {
                        nonexisting.Add(email,name);
                    }
                }

                //Create participants from non existing mails
                foreach (KeyValuePair<string,string> kv in nonexisting) {
                    string email = kv.Key;
                    string name = kv.Value;

                    Participant p = new Participant {
                        Email = email,
                        AccessToken = WiseGuyUtils.CreateToken(),
                        Name = name,
                        Created = DateTime.Now,
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