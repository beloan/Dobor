using Application.Abstractions.RepositoryAbstractions;
using Application.Abstractions.ServiceAbstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class HangfireService : IHangfireService
    {
        IAssigmentService _assigmentService;
        IJobListRepository _jobListRepos;
        IUnitOfWork _unitOfWork;
        ILogger<IHangfireService> _logger;
        IServiceProvider _serviceProvider;

        public HangfireService(IAssigmentService assigmentService, IJobListRepository jobListRepos, IUnitOfWork unitOfWork, ILogger<IHangfireService> logger, IServiceProvider serviceProvider)
        {
            _assigmentService = assigmentService;
            _jobListRepos = jobListRepos;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _serviceProvider = serviceProvider;

        }

        public async void AddAssigmentByJobList()
        {
            var date = DateTime.UtcNow;

            
                var jobList = await _jobListRepos.GetAll();
                foreach (var item in jobList)
                {
                    var day = 7 + (int)Enum.Parse<DayOfWeek>(item.Day!);
                    day = day - (int)Enum.Parse<DayOfWeek>(date.DayOfWeek.ToString());
                    var date1 = date.AddDays(day);
                    var date2 = date.AddDays(7);

                    if (item is not null)
                    {
                        await _assigmentService
                        .AddAssigment(
                            new Application.Models.RequestModels.Assigment
                            {
                                FormId = item.FormId,
                                Date = DateOnly.FromDateTime(date1)
                            });

                        await _assigmentService
                            .AddAssigment(
                                new Application.Models.RequestModels.Assigment
                                {
                                    FormId = item.FormId,
                                    Date = DateOnly.FromDateTime(date2)
                                });
                    }
                }
                _logger.LogInformation("Добавлены tasks по расписанию. Дата: {0}", date);

                await _unitOfWork.SaveChangesAsync();
            }
        }
    
}
