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
            public List<Phrase> phrases {get;set;} = new List<Phrase>();
        }

        public EvaluationController(ILogger<EvaluationController> logger)
        {
        }

        [HttpGet("{copyToken}")]
        public async Task<ActionResult<SheetCopy>> Get(string copyToken)
        {
            using (var context = new WiseGuyContext()) 
            {
                var copy = context.Copies
                    .Include(c=>c.SheetTemplate)
                    .Include(c=>c.SheetTemplate.Phrases)
                    .First(c => c.Token == copyToken);

                var form = new form();
                form.course = copy.Course;
                form.subject = copy.Subject;

                foreach (var p in copy.SheetTemplate.Phrases) {
                    var tmp = p;
                    tmp.SheetTemplate=null;
                    form.phrases.Add(p);
                }
                return Ok(form);
            }
        }
    }
}
