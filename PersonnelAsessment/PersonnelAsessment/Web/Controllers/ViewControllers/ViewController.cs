using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.ViewControllers
{
    public class ViewController : ControllerBase
    {
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => File("index.html", "text/html"));
        }

        [Route("/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            return await Task.Run(() => File("src/html/login.html", "text/html"));
        }

        [HttpGet("password-creator")]
        public async Task<IActionResult> PasswordCreator()
        {
            return await Task.Run(() => File("src/html/password-creator.html", "text/html"));
        }

        [HttpGet("forget-password")]
        public async Task<IActionResult> PasswordForgetting()
        {
            return await Task.Run(() => File("src/html/forget-password.html", "text/html"));
        }
    }
}
