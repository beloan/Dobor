using Application.Abstractions.RepositoryAbstractions;
using Application.Abstractions.ServiceAbstractions;
using Application.Models.ResponseModels;
using Domain.EntityProperties;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("form/")]
    public class FormController : ControllerBase
    {
        IFormService _formService;
        IFormRepository _formRepository;
        IOrganisationService _organisationService;

        public FormController(IFormService formService, IOrganisationService organisationService, IFormRepository formRepository)
        {
            _formService = formService;
            _formRepository = formRepository;
            _organisationService = organisationService;
        }

        [HttpPost("add")]
        [Authorize(Roles = Roles.Organisation)]
        public async Task<IActionResult> Add(Application.Models.RequestModels.Form form)
        {
            try
            {

                var org = await _organisationService.GetOrganisationByEmail(User.FindFirstValue(ClaimTypes.Email));
                if (org is null) return Unauthorized();
                var result = await _formService.AddForm(form, org.Id);

                return Ok(result);

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
            var result = await _formService.GetForm(id);

            return result is null
                ? NotFound()
                : Ok(result);
        }

        [HttpPatch("{id:int}/edit")]
        [Authorize(Roles = Roles.Organisation)]
        public async Task<IActionResult> Edit(int id, Application.Models.RequestModels.Form form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var email = User.FindFirstValue(ClaimTypes.Email);
                    var role = User.FindFirst(ClaimTypes.Role);

                    if (role is null || email is null) return Unauthorized();

                    var realForm = await _formRepository.GetById(id);
                    if (realForm is null) return BadRequest();

                    bool can = realForm.Organisation!.Email!.Equals(email);

                    var result = can
                        ? await _formService.UpdateForm(id, form)
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
        [Authorize(Roles = Roles.Admin + ", " + Roles.Organisation)]
        public async Task<IActionResult> Delete(int id)
        {
            var form = await _formRepository.GetById(id);
            if (form is null) return BadRequest();

            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email is null) return Unauthorized();

            var isOrgCan = form.Organisation!.Email!.Equals(email);

            return isOrgCan && await _formService.DeleteForm(id)
                ? Ok()
                : BadRequest();
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetForms([FromQuery]int? OrganisationId, [FromQuery]int? LeadId)
        {
            if (OrganisationId is not null)
            {
                if (LeadId is not null)
                    return Ok(await _formService.GetAllFormsByLeadAndOrganisation(LeadId.Value, OrganisationId.Value));
                return Ok(await _formService.GetAllFormsByOrganisation(OrganisationId.Value));
            }
            else if (LeadId is not null) return Ok(await _formService.GetAllFormsByLead(LeadId.Value));
            else return Ok(await _formService.GetAllForms());
        }
    }
}
