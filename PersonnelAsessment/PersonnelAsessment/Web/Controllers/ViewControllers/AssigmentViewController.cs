using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.ViewControllers
{
    [Route("assigment/")]
    public class AssigmentViewController : ControllerBase
    {
        [HttpGet("main")]
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => File("/src/html/assigment/main.html", "text/html"));
        }
    }
}
