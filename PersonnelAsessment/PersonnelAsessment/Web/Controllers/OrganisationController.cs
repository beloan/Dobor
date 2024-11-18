using Application.Abstractions.ServiceAbstractions;
using Application.Models.ResponseModels;
using Domain.EntityProperties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("org/")]
    public class OrganisationController : ControllerBase
    {
        IOrganisationService _organisationService;
        IUserService _userService;

        public OrganisationController(IOrganisationService organisationService, IUserService userService)
        {
            _organisationService = organisationService;
            _userService = userService;
        }

        [HttpPost("reg")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(Application.Models.RequestModels.Organisation org)
        {
            try
            {
                var result = await _organisationService.RegisterOrganisation(org);

                return Redirect("/login");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _organisationService.GetOrganisationById(id);

            return result is null
                ? NotFound()
                : Ok(result);
        }

        [HttpPatch("{id:int}/edit")]
        [Authorize]
        public async Task<IActionResult> Edit(int id, Application.Models.RequestModels.Organisation organisation)
        {
            var result = await _organisationService.UpdateOrganisation(id, organisation);

            return result is null
                ? BadRequest()
                : Ok(result);
        }

        [HttpDelete("{id:int}/del")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _organisationService.DeleteOrganisation(id);

            return result
                ? Ok()
                : BadRequest();
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetOrganisations()
        {
            return Ok(await _organisationService.GetAllOrganisation());
        }

        [HttpGet("{id:int}/sendconfirmation")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> SendConfirmation(int id)
        {
            return await _organisationService.SendConfirmationMessage((await _userService.GetUserById(id))!.Email)
                ? Ok()
                : BadRequest();
        }
    }
}
