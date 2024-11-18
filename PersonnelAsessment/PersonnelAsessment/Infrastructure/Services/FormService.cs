using Application.Abstractions.RepositoryAbstractions;
using Application.Abstractions.ServiceAbstractions;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class FormService : IFormService
    {
        IFormRepository _repos;
        IMapper _mapper;
        IJobListService _jobListService;
        IUnitOfWork _unitOfWork;

        public FormService(IFormRepository formRepository, IMapper mapper, IJobListService jobListService, IUnitOfWork unitOfWork)
        {
            _repos = formRepository;
            _mapper = mapper;
            _jobListService = jobListService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Application.Models.ResponseModels.Form> AddForm(Application.Models.RequestModels.Form form, int organisationId)
        {
            form.OrganisationId = organisationId;
            if (form.Litera == '\0') form.Litera = ' ';
            var result = await _repos.Add(_mapper.Map<Form>(form));
            await _unitOfWork.SaveChangesAsync();

            for (int i = 0; i < 7; i++)
            {
                for(int j = 1; j <= 8; j++)
                {
                    await _jobListService
                    .AddJobList(
                        new Application.Models.RequestModels.JobList
                        {
                            Day = ((DayOfWeek)i).ToString(),
                            FormId = result.Id,
                            IndexNum = j
                        });
                }
            }

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Application.Models.ResponseModels.Form>(result);
        }

        public async Task<bool> DeleteForm(int id)
        {
            var result = await _repos.DeleteById(id);
            await _unitOfWork.SaveChangesAsync();

            return result == 1;
        }

        public async Task<List<Application.Models.ResponseModels.Form>?> GetAllForms()
        {
            var result = await _repos.GetAll();
            result.Sort((a, b) => a.Number - b.Number);
            return _mapper.Map<List<Application.Models.ResponseModels.Form>>(result);
        }

        public async Task<List<Application.Models.ResponseModels.Form>?> GetAllFormsByOrganisation(int organisationId)
        {
            var result = (await _repos.GetAll())
                .Where(x => x.OrganisationId == organisationId).ToList();
            result.Sort((a, b) => a.Number - b.Number);

            return _mapper.Map<List<Application.Models.ResponseModels.Form>>(result);
        }

        public async Task<List<Application.Models.ResponseModels.Form>?> GetAllFormsByLead(int leadId)
        {
            var result = (await _repos.GetAll())
                .Where(x => x.LeadId == leadId).ToList();
            result.Sort((a, b) => a.Number - b.Number);

            return _mapper.Map<List<Application.Models.ResponseModels.Form>>(result);
        }

        public async Task<List<Application.Models.ResponseModels.Form>?> GetAllFormsByLeadAndOrganisation(int leadId, int organisationId)
        {
            return _mapper.Map<List<Application.Models.ResponseModels.Form>>((await _repos.GetAll())
                .Where(x => x.OrganisationId == organisationId && x.LeadId == leadId));
        }

        public async Task<Application.Models.ResponseModels.Form?> GetForm(int id)
        {
            return _mapper.Map<Application.Models.ResponseModels.Form>(await _repos.GetById(id));
        }

        public async Task<Application.Models.ResponseModels.Form?> UpdateForm(int id, Application.Models.RequestModels.Form form)
        {
            var result = await _repos.UpdateById(id, _mapper.Map<Form>(form));
            await _unitOfWork.SaveChangesAsync();
            
            return result == 1
                ? await GetForm(id)
                : null;
        }
    }
}
