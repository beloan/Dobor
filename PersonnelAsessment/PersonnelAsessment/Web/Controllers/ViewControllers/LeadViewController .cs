using Domain.EntityProperties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.ViewControllers
{
    [Route("lead/")]
    public class LeadViewController : ControllerBase
    {
        [HttpGet("profile")]
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => File("/src/html/lead/profile.html", "text/html"));
        }

        [HttpGet("reg")]
        [Authorize(Roles = Roles.Organisation + "," + Roles.Admin)]
        public async Task<IActionResult> Registration()
        {
            return await Task.Run(() => File("/src/html/lead/reg.html", "text/html"));
        }

        [HttpGet("myteams")]
        public async Task<IActionResult> MyTeams()
        {
            return await Task.Run(() => File("/src/html/lead/classes.html", "text/html"));
        }

        [HttpGet("assigment")]
        public async Task<IActionResult> Assigments()
        {
            return await Task.Run(() => File("/src/html/lead/assigments.html", "text/html"));
        }

        [HttpGet("grades")]
        public async Task<IActionResult> Grades()
        {
            return await Task.Run(() => File("/src/html/lead/grades.html", "text/html"));
        }
    }
}
