using Application.Abstractions.ServiceAbstractions;
using Domain.EntityProperties;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("admin/")]
    public class AdminController : ControllerBase
    {
        IAdminService _adminService;

        public AdminController(IAdminService adminService, IEmailService emailService)
        {
            _adminService = adminService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Application.Models.RequestModels.Admin admin)
        {
            try
            {
                var realAdmin = await _adminService.LoginAdmin(admin);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, realAdmin.Email!),
                    new Claim(ClaimTypes.Role, "admin")
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                Response.Cookies.Append("email", realAdmin.Email!);
                return Redirect("/admin");
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpGet("logout")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            Response.Cookies.Delete("email");

            return Redirect("/admin/login");
        }
    }
}
