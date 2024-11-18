using Application.Abstractions.ServiceAbstractions;
using Application.Models.ResponseModels;
using Domain.EntityProperties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("grd/")]
    public class GradeController : ControllerBase
    {
        IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpPost("{id:int}/edit")]
        [Authorize(Roles = Roles.Lead)]
        public async Task<IActionResult> Edit(int id, Application.Models.RequestModels.Grade grade)
        {
            try
            {
                var result = await _gradeService.UpdateGrade(id, grade);

                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetStudentGrades([FromQuery] int? WorkerId)
        {
            return WorkerId is null
                ? BadRequest()
                : Ok(await _gradeService.GetAllGradesByWorker(WorkerId.Value));
        }

        [HttpGet("")]
        [Authorize]
        public async Task<IActionResult> GetGradeByParams([FromQuery] int? WorkerId, [FromQuery] int? AssigmentId)
        {
            return WorkerId is not null && AssigmentId is not null
                ? Ok(await _gradeService.GetGradeByWorkerAndAssigment(WorkerId.Value, AssigmentId.Value))
                : BadRequest();
        }
    }
}
