using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IDoctorServiceRepository
    {
        void CreateNewService(DoctorService doctorService);
        void DeleteService(DoctorService doctorService);
        Task<IEnumerable<DoctorServiceDto>> GetDoctorServices(int doctorId);
        Task<DoctorService> GetDoctorService(int serviceId);
        Task<bool> SaveAllAsync();
    }
}