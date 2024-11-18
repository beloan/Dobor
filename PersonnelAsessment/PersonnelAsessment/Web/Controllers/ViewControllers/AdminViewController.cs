using Domain.EntityProperties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.ViewControllers
{
    [Route("admin/")]
    public class AdminViewController : ControllerBase
    {
        [HttpGet("")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> IndexAdmin()
        {
            return await Task.Run(() => File("/admin/admin.html", "text/html"));
        }

        [HttpGet("login")]
        public async Task<IActionResult> LoginAdmin()
        {
            return await Task.Run(() => File("/admin/login.html", "text/html"));
        }
    }
}
