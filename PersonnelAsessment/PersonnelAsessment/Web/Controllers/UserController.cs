using Application.Abstractions.ServiceAbstractions;
using Domain.EntityProperties;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Abstractions.ServiceAbstractions;

namespace Web.Controllers
{
    [Route("user/")]
    public class UserController : ControllerBase
    {
        IUserService _userService;
        IFileUploadService _fileService;
        ILogger<UserController> _logger;

        public UserController(IUserService userService, IFileUploadService fileUploadService, ILogger<UserController> logger)
        {
            _userService = userService;
            _fileService = fileUploadService;
            _logger = logger;
        }

        [HttpPost("reg")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(Application.Models.RequestModels.User user)
        {
            try
            {
                var result = await _userService.RegisterUser(user);

                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Application.Models.RequestModels.User user)
        {
            //_logger.LogInformation("Вход");

            try
            {
                var realUser = await _userService.LoginUser(user);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, realUser.Email!),
                    new Claim(ClaimTypes.Role, realUser.Role!)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                Response.Cookies.Append("role", realUser.Role!);
                Response.Cookies.Append("email", realUser.Email!);
                Response.Cookies.Append("id", realUser.Id.ToString());

                return RedirectPermanent("/");
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            Response.Cookies.Delete("role");
            Response.Cookies.Delete("email");
            Response.Cookies.Delete("id");

            return Redirect("/login");
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPatch("{id:int}/edit")]
        [Authorize(Roles = Roles.Admin + ", " + Roles.Organisation)]
        public async Task<IActionResult> EditUser(int id, Application.Models.RequestModels.User user)
        {
            var userId = (HttpContext.User.Identity as ClaimsIdentity)!.FindFirst("email");
            var result = await _userService.UpdateUser(id, user);

            return result is null
                ? BadRequest()
                : Ok(result);
        }

        [HttpDelete("{id:int}/del")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            return result
                ? BadRequest()
                : Ok();
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
        {
            if (token is null) return BadRequest();

            Response.Cookies.Append("token", token);

            return Redirect("/password-creator");
        }

        [HttpPost("password/change")]
        public async Task<IActionResult> ChangePassword(Application.Models.RequestModels.User user)
        {
            var token = Request.Cookies["token"];
            if (token is null) return BadRequest();

            var email = await _userService.GetUserEmailByToken(token);
            if (email is null) return BadRequest();

            var result = await _userService.SetPassword(email!, user.Password!);

            if (result)
            {
                var res = await _userService!.ActivateUser(email!);

                if (res is null) return BadRequest();
                else return Redirect("/");
            }
            else return BadRequest();
        }

        [HttpPost("{id:int}/image/add")]
        [Authorize]
        public async Task<IActionResult> UploadProfileImage(int id, IFormFile file)
        {
            var result = await _fileService.UploadFile(file);

            return await _userService.AddUserImage(id, result!)
                ? Redirect("/")
                : BadRequest();
        }

        [HttpGet("{id:int}/image/name")]
        [Authorize]
        public async Task<IActionResult> GetProfileImage(int id)
        {
            return Ok(await _userService.GetUserImagePath(id));
        }

        [HttpGet("all")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("{id:int}/sendconfirmmessage")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> SendConfirmationMessage(int id)
        {
            await _userService.SendConfirmationMessage((await _userService.GetUserById(id))!.Email!);
            return Ok();
        }

        [HttpGet("/sendconfirmmessage")]
        public async Task<IActionResult> SendConfirmationMessage(Application.Models.RequestModels.User user)
        {
            await _userService.SendConfirmationMessage(user.Email);
            return Redirect("/login");
        }
    }
}
