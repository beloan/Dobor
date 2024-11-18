using Domain.EntityProperties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.ViewControllers
{
    [Route("worker/")]
    public class WorkertViewController : ControllerBase
    {
        [HttpGet("reg")]
        [Authorize(Roles = Roles.Organisation + "," + Roles.Admin)]
        public async Task<IActionResult> Registration()
        {
            return await Task.Run(() => File("/src/html/worker/reg.html", "text/html"));
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => File("/src/html/worker/profile.html", "text/html"));
        }

        [HttpGet("diary")]
        public async Task<IActionResult> Diary()
        {
            return await Task.Run(() => File("/src/html/worker/diary.html", "text/html"));
        }

        [HttpGet("summary")]
        public async Task<IActionResult> Summary()
        {
            return await Task.Run(() => File("/src/html/worker/summary.html", "text/html"));
        }

        [HttpGet("jobList")]
        public async Task<IActionResult> jobList()
        {
            return await Task.Run(() => File("/src/html/worker/jobList.html", "text/html"));
        }
    }
}
