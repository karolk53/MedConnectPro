using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
        public interface IDoctorRepository
        {
                public Task<IEnumerable<DoctorListDto>> GetDoctorsListAsync();
                public Task<Doctor> GetDoctorById(int id);
                public Task<DoctorDto> GetDoctorByIdAsync(int id);
                public Task<Doctor> GetDoctorWithSpecialisation(int id);
                public Task<bool> SaveAllAsync();

        }
}