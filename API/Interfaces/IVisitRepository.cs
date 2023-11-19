using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IVisitRepository
    {
        void AddNewVisit(Visit visit);
        Task<Visit> GetVisitById(int visitId);
        Task<IEnumerable<VisitDto>> GetPatientVisitsList(int patientId);
        Task<IEnumerable<VisitDto>> GetDoctorVisitsList(int doctorId);
        Task<bool> SaveAllAsync();
    }
}