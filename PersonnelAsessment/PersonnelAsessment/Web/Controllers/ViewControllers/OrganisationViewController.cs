using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.ViewControllers
{
    [Route("organisation/")]
    public class OrganisationViewController : ControllerBase
    {
        [HttpGet("profile")]
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => File("/src/html/org/profile.html", "text/html"));
        }

        [HttpGet("reg")]
        public async Task<IActionResult> Registration()
        {
            return await Task.Run(() => File("/src/html/org/reg.html", "text/html"));
        }

        [HttpGet("leads")]
        public async Task<IActionResult> Leads()
        {
            return await Task.Run(() => File("src/html/org/leads.html", "text/html"));
        }

        [HttpGet("workers")]
        public async Task<IActionResult> Workers()
        {
            return await Task.Run(() => File("src/html/org/workers.html", "text/html"));
        }

        [HttpGet("jobList")]
        public async Task<IActionResult> JobList()
        {
            return await Task.Run(() => File("src/html/org/jobList.html", "text/html"));
        }


        [HttpGet("forms")]
        public async Task<IActionResult> Forms()
        {
            return await Task.Run(() => File("src/html/org/forms.html", "text/html"));
        }
    }
}
