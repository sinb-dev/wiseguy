using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
                var copy = context.Copies.First(c => c.Token == copyToken);
                return Ok(copy);
            }
        }
    }
}
