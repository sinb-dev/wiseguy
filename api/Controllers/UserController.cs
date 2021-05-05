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
    [Route("user")]
    public class UserController : ControllerBase
    {
        public UserController(ILogger<EvaluationController> logger)
        {
        }

        [HttpPost("{session}/create")]
        public async Task<ActionResult<String>> CreateUser(string session, [FromForm]IDictionary<string, string> data)
        {
            if (!IsLoggedIn(session)) return Unauthorized();
            using(var context = new WiseGuyContext()) {
                string username = data.ContainsKey("username") ? data["username"] : "";
                string password = data.ContainsKey("password") ? data["password"] : "";
                string email = data.ContainsKey("email") ? data["email"] : "";
                if (String.IsNullOrWhiteSpace(username)) return ValidationProblem("Username is empty");
                if (String.IsNullOrWhiteSpace(password)) return ValidationProblem("Password is empty");
                if (String.IsNullOrWhiteSpace(email)) return ValidationProblem("Email is empty");
                User user = new User {
                    Username = username,
                    Password = password,
                    Created = DateTime.Now,
                    Email = email,
                    Session = WiseGuyUtils.CreateToken(),
                    LastActivity = DateTime.Now
                };
                await context.SaveChangesAsync();
                return Ok(user.Session);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<String>> LogUserIn([FromForm]IDictionary<string, string> data)
        {
            using(var context = new WiseGuyContext()) {
                string password = data.ContainsKey("password") ? data["password"] : "";
                string username = data.ContainsKey("username") ? data["username"] : "";
                if (String.IsNullOrWhiteSpace(password)) return ValidationProblem("Password is empty");
                if (String.IsNullOrWhiteSpace(username)) return ValidationProblem("Username is empty");
                User user = null;
                try {
                    user = context.Users.Where(u => u.Password == password && u.Username == username).First();
                } catch {
                    return ValidationProblem("No such user or password");
                }
                user.Session = WiseGuyUtils.CreateToken();
                user.LastActivity = DateTime.Now;
                await context.SaveChangesAsync();
                return Ok(user.Session);
            }
        }
        [HttpGet("login/{session}")]
        public static ActionResult<bool> requestAccessStatus(string session) 
        {
            if (IsLoggedIn(session))
                return true;
            return false;
        }
        [HttpGet("logout/{session}")]
        public static ActionResult<string> requestLogout(string session) 
        {
            using(var context = new WiseGuyContext()) {
                try {
                    var user = context.Users.Where(u => u.Session == session).First();
                    user.Session = "";
                    context.SaveChanges();
                    return "ok";
                } catch {

                }
            }
            return "error";
        }
        public static bool IsLoggedIn(string session) 
        {
            using(var context = new WiseGuyContext()) {
                try {
                    var user = context.Users.Where(u => u.Session == session && (DateTime.Now-u.LastActivity).Minutes < 15);
                } catch {
                    return false;
                }
                return true;
            }
        }
    }
}
