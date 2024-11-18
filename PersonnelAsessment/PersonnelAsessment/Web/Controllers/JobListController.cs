using Application.Abstractions.RepositoryAbstractions;
using Application.Abstractions.ServiceAbstractions;
using Domain.EntityProperties;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("jList/")]
    public class JobListController : ControllerBase
    {
        IJobListService _jobListService;
        IJobListRepository _jobListRepository;
        IHtmlToDocConverter _htmlToDocConverter;

        public JobListController(IJobListService jobListService, IJobListRepository jobListRepository, IHtmlToDocConverter htmlToDocConverter)
        {
            _jobListService = jobListService;
            _jobListRepository = jobListRepository;
            _htmlToDocConverter = htmlToDocConverter;
        }

        [HttpPost("{id:int}/edit")]
        [Authorize(Roles = Roles.Organisation)]
        public async Task<IActionResult> Edit(int id, Application.Models.RequestModels.JobList jobList)
        {
            try
            {
                var email = User.FindFirstValue(ClaimTypes.Email);
                var role = User.FindFirst(ClaimTypes.Role);

                if (role is null || email is null) return Unauthorized();

                var realJobListItem = await _jobListRepository.GetById(id);
                if (realJobListItem is null) return BadRequest();

                bool can = realJobListItem.Form!.Organisation!.Email!.Equals(email);
                var result = can
                    ? await _jobListService.UpdateJobList(id, jobList)
                    : null;

                return result is null
                    ? BadRequest()
                    : Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        
        [HttpGet("all")]
        public async Task<IActionResult> GetJobList([FromQuery] int? FormId, [FromQuery] string? Day)
        {
            if (Day is not null && FormId is not null)
                return Ok(await _jobListService.GetAllJobListsByFormAndDay(Day, FormId.Value));
            else if (FormId is not null)
                return Ok(await _jobListService.GetAllJobListsByForm(FormId.Value));
            return BadRequest();
        }

        [HttpGet("download")]
        [Authorize(Roles = Roles.Organisation)]
        public async Task<IActionResult> DownloadSchedule()
        {
            var html = await _jobListService.GetHtmlTable(User.FindFirstValue(ClaimTypes.Email));

            var file = await Task.Run(() => _htmlToDocConverter.Convert(html!));

            return File(file!, "application/docx");
        }
    }
}
