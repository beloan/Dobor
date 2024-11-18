using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.ViewControllers
{
    [Route("programm/")]
    public class ProgrammViewController : ControllerBase
    {
        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            return await Task.Run(() => File("/src/html/programm/create.html", "text/html"));
        }
        [HttpGet("main")]
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => File("/src/html/programm/main.html", "text/html"));
        }
    }
}
