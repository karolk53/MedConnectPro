using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen( c => 
            {
                c.SwaggerDoc("v1",new OpenApiInfo{Title="MedConnectPro", Version="v1"});
            });
            services.AddDbContext<DataContext>(opt => {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ISpecialisationRepository, SpecialisationRepository>();
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<INotesRepository, NotesRepository>();
            services.AddScoped<IDoctorServiceRepository, DoctorServiceRepository>();
            services.AddScoped<IOfficeRepository, OfficeRepository>();
            services.AddScoped<IVisitRepository, VisitRepository>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();

            return services;
        }
    }
}