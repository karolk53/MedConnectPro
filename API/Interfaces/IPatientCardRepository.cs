using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IPatientCardRepository
    {
        void AddNewCard(PatientCard patientCard);
        Task<IEnumerable<PatientCardList>> GetDoctorCards(int doctorId, CardParams cardParams);
        Task<bool> SaveAllAsync();
    }
}