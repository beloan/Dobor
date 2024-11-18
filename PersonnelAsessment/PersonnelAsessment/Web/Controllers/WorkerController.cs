using Application.Abstractions.RepositoryAbstractions;
using Application.Abstractions.ServiceAbstractions;
using Application.Models.ResponseModels;
using Domain.EntityProperties;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("wrk/")]
    public class WorkerController : ControllerBase
    {
        IWorkerService _workerService;
        IFormRepository _formRepository;
        IWorkerRepository _workerRepository;

        public WorkerController(IWorkerService workerService, IFormRepository formRepository, IWorkerRepository workerRepository)
        {
            _workerService = workerService;
            _formRepository = formRepository;
            _workerRepository = workerRepository;
        }

        [HttpPost("reg")]
        [Authorize(Roles = Roles.Organisation)]
        public async Task<IActionResult> Register(Application.Models.RequestModels.Worker worker)
        {
            try
            {
                var form = await _formRepository.GetById(worker.FormId);
                var can = form is not null && form.Organisation!.Email!.Equals(User.FindFirstValue(ClaimTypes.Email));

                var result = can
                    ? await _workerService.RegisterWorker(worker)
                    : null;

                return Redirect("/");
            }
            catch
            {
                return Redirect("/");
            }
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _workerService.GetWorkerById(id);

            return result is null
                ? NotFound()
                : Ok(result);
        }

        [HttpPatch("{id:int}/edit")]
        [Authorize(Roles = Roles.Organisation + ", " + Roles.Worker)]
        public async Task<IActionResult> Edit(int id, Application.Models.RequestModels.Worker worker)
        {
            if (ModelState.IsValid)
            {
                var email = User.FindFirstValue(ClaimTypes.Email);
                var role = User.FindFirst(ClaimTypes.Role);
                if (role is null || email is null) return Unauthorized();

                var realStudent = await _workerRepository.GetById(id);
                if (realStudent is null) return BadRequest();

                bool can;

                if (role.Value.Equals(Roles.Organisation))
                {
                    can = realStudent.Form!.Organisation!.Email!.Equals(email);
                }
                else
                {
                    can = realStudent.Email!.Equals(email);
                }
                var result = can
                    ? await _workerService.UpdateWorker(id, worker)
                    : null;

                return result is null
                    ? BadRequest()
                    : Ok(result);
            }
            return ValidationProblem(new ValidationProblemDetails(ModelState));
        }

        [HttpDelete("{id:int}/del")]
        [Authorize]
        [Authorize(Roles = Roles.Admin + "," + Roles.Organisation)]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _workerRepository.GetById(id);
            if (student is null) return BadRequest();

            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email is null) return Unauthorized();

            var isOrgCan = student.Form!.Organisation!.Email!.Equals(email);

            return isOrgCan && await _workerService.DeleteWorker(id)
                ? Ok()
                : BadRequest();
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] int? OrganisationId, [FromQuery] int? FormId)
        {
            if (FormId is not null)
            {
                return Ok(await _workerService.GetAllWorkersByForm(FormId.Value));
            }
            else if (OrganisationId is not null)
            {
                return Ok(await _workerService.GetAllWorkersByOrganisation(OrganisationId.Value));
            }
            else return Ok(await _workerService.GetAllWorkers());
        }
    }
}
