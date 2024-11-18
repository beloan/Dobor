using Application.Abstractions.RepositoryAbstractions;
using Application.Abstractions.ServiceAbstractions;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Web.Abstractions.ServiceAbstractions;
using Web.Services;

namespace Web.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IFormRepository, FormRepository>();
            services.AddScoped<IGradeRepository, GradeRepository>();
            services.AddScoped<IAssigmentRepository, AssigmentRepository>();
            services.AddScoped<IOrganizationRepository, OrganisationRepository>();
            services.AddScoped<IJobListRepository, JobListRepository>();
            services.AddScoped<IWorkerRepository, WorkerRepository>();
            services.AddScoped<ILeadRepository, LeadRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserImageRepository, UserImageRepository>();

            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<IGradeService, GradeService>();
            services.AddScoped<IHasherService, HasherService>();
            services.AddScoped<IAssigmentService,  AssigmentService>();
            services.AddScoped<IOrganisationService, OrganisationService>();
            services.AddScoped<IJobListService,  JobListService>();
            services.AddScoped<IWorkerService, WorkerService>();
            services.AddScoped<ILeadService, LeadService>();
            services.AddScoped<IUserService, UserService>();
            
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPasswordGenerator, PasswordGenerator>();
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IHtmlToDocConverter, HtmlToDocConverter>();
            services.AddScoped<IHangfireService, HangfireService>();
            services.AddScoped<IFileUploadService, FileUploadService>();

            return services;
        }
    }
}
