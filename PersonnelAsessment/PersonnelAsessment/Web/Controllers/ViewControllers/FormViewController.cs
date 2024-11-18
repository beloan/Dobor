using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.ViewControllers
{
    [Route("form/")]
    public class FormViewController : ControllerBase
    {


        [HttpGet("main")]
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => File("/src/html/form/main.html", "text/html"));
        }
    }
}
