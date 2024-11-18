using Application.Abstractions.RepositoryAbstractions;
using Application.Abstractions.ServiceAbstractions;
using AutoMapper;
using Domain.Entities;
using System;
using System.Text;

namespace Infrastructure.Services
{
    public class JobListService : IJobListService
    {
        IJobListRepository _repos;
        IMapper _mapper;
        IAssigmentService _assigmentService;
        IUserService _userService;
        IUnitOfWork _unitOfWork;

        public JobListService(IJobListRepository jobListRepository, IMapper mapper, IAssigmentService assigmentService, IUserService userService, IUnitOfWork unitOfWork)
        {
            _repos = jobListRepository;
            _mapper = mapper;
            _assigmentService = assigmentService;
            _userService = userService;
            _unitOfWork = unitOfWork;

        }

        public async Task<Application.Models.ResponseModels.JobList> AddJobList(Application.Models.RequestModels.JobList jobList)
        {
            var result = await _repos.Add(_mapper.Map<JobList>(jobList));
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Application.Models.ResponseModels.JobList>(result);
        }

        public async Task<bool> DeleteJobList(int id)
        {
            var result = await _repos.DeleteById(id);
            await _unitOfWork.SaveChangesAsync();

            return result == 1;
        }

        public async Task<List<Application.Models.ResponseModels.JobList>?> GetAllJobListsByForm(int formId)
        {
            return _mapper.Map<List<Application.Models.ResponseModels.JobList>>((await _repos.GetAll())
                .Where(x => x.FormId == formId).ToList());
        }

        public async Task<List<Application.Models.ResponseModels.JobList>?> GetAllJobListsByFormAndDay(string day, int formId)
        {
            return _mapper.Map<List<Application.Models.ResponseModels.JobList>>((await _repos.GetAll())
                .Where(x => x.FormId == formId && x.Day!.Equals(day)).ToList());
        }

        public async Task<string?> GetHtmlTable(int organisationId)
        {
            var jobList = (await _repos.GetAll()).Where(x => x.Form!.OrganisationId == organisationId).ToList();

            var forms = jobList.Select(x => x.Form).DistinctBy(x => x!.Id).ToList();
            var days = jobList.Select(x => x.Day).Distinct().ToList();
            var indexes = jobList.Select(x => x.IndexNum).Distinct().ToList();

            var result = new StringBuilder();

            foreach(var day in days)
            {
                result.AppendLine($"<h5>{day}</h5>");

                result.Append("<table>");

                result.Append("<tr>");
                foreach (var form in forms)
                {
                    result.Append($"<th style=\"width: 100px\"><b>{form!.Number}</b></th>");
                }
                result.Append("</tr>");

                foreach (var index in indexes)
                {
                    result.Append("<tr>");
                    foreach(var form in forms)
                    {
                        var asse = jobList
                            .FirstOrDefault(x => x.FormId == form!.Id && x.IndexNum == index && x.Day!.Equals(day));

                    }
                    result.Append("</tr>");
                }

                result.Append("</table>");
                result.Append("<hr>");
            }

            return result.ToString();
        }

        public async Task<string?> GetHtmlTable(string? email)
        {
            return await GetHtmlTable((await _userService.GetUserByEmail(email))!.Id);
        }

        public async Task<Application.Models.ResponseModels.JobList?> GetJobList(int id)
        {
            return _mapper.Map<Application.Models.ResponseModels.JobList>(await _repos.GetById(id));
        }

        public async Task<Application.Models.ResponseModels.JobList?> UpdateJobList(int id, Application.Models.RequestModels.JobList jobList)
        {
            var result = await _repos.UpdateById(id, _mapper.Map<JobList>(jobList)) == 1;
            await _unitOfWork.SaveChangesAsync();

            if(result && jobList is not null)
            {
                var day = 7 + (int)Enum.Parse<DayOfWeek>(jobList.Day!);
                var date = DateTime.UtcNow;
                day = day - (int)Enum.Parse<DayOfWeek>(date.DayOfWeek.ToString());
                date.AddDays(day);

                await _assigmentService
                    .AddAssigment(
                        new Application.Models.RequestModels.Assigment
                        {
                            FormId = jobList.FormId,
                            Date = DateOnly.FromDateTime(date)
                        });
                await _unitOfWork.SaveChangesAsync();
            }

            await _unitOfWork.SaveChangesAsync();

            return result
                ? await GetJobList(id)
                : null;
        }
    }
}
