using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IDoctorRepository
    {
        public Task<PagedList<DoctorListDto>> GetDoctorsListAsync(DoctorParams doctorParams); 
        public Task<Doctor> GetDoctorById(int id);
        public Task<DoctorDto> GetDoctorByIdAsync(int id);
        public Task<Doctor> GetDoctorWithSpecialisation(int id);
        public Task<bool> SaveAllAsync();

    }
}