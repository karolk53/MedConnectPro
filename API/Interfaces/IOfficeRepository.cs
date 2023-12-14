using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IOfficeRepository
    {
        void AddNewOffice(Office office);
        void DeleteOffice(Office office);
        Task<OfficeCreateDto> GetDoctorOffice(int doctorId);
        Task<Office> GetOfficeById(int officeId);
        Task<bool> SaveAllAsync();
    }
}