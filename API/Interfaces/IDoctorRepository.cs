using API.DTOs;

namespace API.Interfaces
{
    public interface IDoctorRepository
    {
        public Task<IEnumerable<DoctorListDto>> GetDoctorsListAsync();
    }
}