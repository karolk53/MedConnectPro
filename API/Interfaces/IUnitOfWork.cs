namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IDoctorRepository DoctorRepository {get;}
        IPatientRepository PatientRepository {get;}
        INotesRepository NotesRepository {get;}
        IOfficeRepository OfficeRepository {get;}
        IPhotoRepository PhotoRepository {get;}
        IVisitRepository VisitRepository {get;}
        ISpecialisationRepository SpecialisationRepository {get;}
        IDoctorServiceRepository DoctorServiceRepository {get;}
        Task<bool> Complete();
        bool HasChanges();
    }
}