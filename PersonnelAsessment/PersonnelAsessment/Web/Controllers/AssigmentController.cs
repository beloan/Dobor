using Application.Abstractions.ServiceAbstractions;
using Domain.EntityProperties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("ass/")]
    public class AssigmentController : ControllerBase
    {
        IAssigmentService _assigmentService;

        public AssigmentController(IAssigmentService assigmentService)
        {
            _assigmentService = assigmentService;
        }

        [HttpPatch("{id:int}/edit")]
        [Authorize(Roles = Roles.Lead)]
        public async Task<IActionResult> Edit(int id, Application.Models.RequestModels.Assigment assigment)
        {
            try
            {
                var result = await _assigmentService.UpdateAssigment(id, assigment);

                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAssigments([FromQuery] int? FormId, [FromQuery] string? Date)
        {
            DateOnly date = new();
            if(DateOnly.TryParse(Date, out date) && FormId is not null)
            {
                return Ok(await _assigmentService.GetAssigmentByDateAndForm(date, FormId.Value));
            }

            return FormId is not null
                ? Ok(await _assigmentService.GetAssigmentByForm(FormId.Value))
                : BadRequest();
        }

        [HttpGet("header")]
        [Authorize]
        public async Task<IActionResult> GetAssigmentsHeaders([FromQuery] int? LeadId)
        {
            return LeadId is null
                ? BadRequest()
                : Ok(await _assigmentService.GetAssigmentHeaderByLead(LeadId.Value));
        }
    }
}
