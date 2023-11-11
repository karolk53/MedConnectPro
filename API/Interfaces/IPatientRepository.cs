using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IPatientRepository
    {
        void Update(Patient patient);

        Task<bool> SaveAllAsync();

        Task<IEnumerable<PatientProfileDto>> GetAllPatientsAsync();

        Task<PatientProfileDto> GetPatientByIdAsync(int id);

        Task<Patient> GetPatientById(int id);

    }
}