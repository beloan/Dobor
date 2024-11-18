using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.ViewControllers
{
    [Route("grade/")]
    public class GradeViewController : ControllerBase
    {
        [HttpGet("table")]
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => File("/src/html/grade/table.html", "text/html"));
        }
    }
}
