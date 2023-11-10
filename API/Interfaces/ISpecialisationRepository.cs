using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ISpecialisationRepository
    {
        void AddSpecialisation(Specialisation specialisation);
        void DeleteSpecialisation(Specialisation specialisation);
        Task<Specialisation> GetSpecialisationById(int id);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<SpecialisationDto>> GetDoctorsSpecialisations(int doctorId);

    }
}