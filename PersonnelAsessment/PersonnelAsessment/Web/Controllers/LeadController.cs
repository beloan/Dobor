using Application.Abstractions.RepositoryAbstractions;
using Application.Abstractions.ServiceAbstractions;
using Domain.EntityProperties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("lea/")]
    public class LeadController : ControllerBase
    {
        ILeadService _leadService;
        ILeadRepository _leadRepository;

        public LeadController(ILeadService leadService, ILeadRepository leadRepository)
        {
            _leadService = leadService;
            _leadRepository = leadRepository;
        }

        [HttpPost("reg")]
        [Authorize(Roles = Roles.Organisation)]
        public async Task<IActionResult> Register(Application.Models.RequestModels.Lead lead)
        {
            if (ModelState.IsValid)
            {
                var result = await _leadService.RegisterLead(lead, User.FindFirstValue(ClaimTypes.Email));

                return Redirect("/");
            }
            return ValidationProblem(new ValidationProblemDetails(ModelState));
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _leadService.GetLeadById(id);

            return result is null
                ? NotFound()
                : Ok(result);
        }

        [HttpPatch("{id:int}/edit")]
        [Authorize(Roles = Roles.Lead + ", " + Roles.Organisation)]
        public async Task<IActionResult> Edit(int id, Application.Models.RequestModels.Lead lead)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var email = User.FindFirstValue(ClaimTypes.Email);
                    var role = User.FindFirst(ClaimTypes.Role);

                    if (role is null || email is null) return Unauthorized();

                    var realTeacher = await _leadRepository.GetById(id);
                    if (realTeacher is null) return BadRequest();

                    bool can;

                    if (role.Value.Equals(Roles.Organisation))
                    {
                        can = realTeacher.Organisations!.FirstOrDefault(org => org.Email!.Equals(email)) is not null;
                    }
                    else
                    {
                        can = realTeacher.Email!.Equals(email);
                    }

                    var result = can
                        ? await _leadService.UpdateLead(id, lead)
                        : null;

                    return result is null
                        ? BadRequest()
                        : Ok(result);
                }

                return ValidationProblem(new ValidationProblemDetails(ModelState));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id:int}/del")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Organisation)]
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _leadRepository.GetById(id);
            if (teacher is null) return BadRequest();

            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email is null) return Unauthorized();

            ///var isOrgCan = teacher.Organisations!.FirstOrDefault(org => org.Email!.Equals(email)) is not null;

            var result = await _leadService.DeleteLead(id);

            return result
                ? Ok()
                : BadRequest();
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] int? OrganisationId)
        {
            return OrganisationId is null
                ? Ok(await _leadService.GetAllLeads())
                : Ok(await _leadService.GetAllLeadsByOrganisation(OrganisationId.Value));
        }
    }
}
