using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IVisitRepository
    {
        void AddNewVisit(Visit visit);
        Task<Visit> GetVisitById(int visitId);
        Task<PagedList<VisitDto>> GetPatientVisitsListAsync(int patientId, VisitParams visitParams);
        Task<IEnumerable<VisitDto>> GetDoctorVisitsList(int doctorId);
        Task<bool> SaveAllAsync();
    }
}