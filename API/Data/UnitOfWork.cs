using API.Interfaces;
using AutoMapper;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UnitOfWork(DataContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
            
        }

        public IDoctorRepository DoctorRepository => new DoctorRepository(_context, _mapper);

        public IPatientRepository PatientRepository => new PatientRepository(_context, _mapper);

        public INotesRepository NotesRepository => new NotesRepository(_context, _mapper);

        public IOfficeRepository OfficeRepository => new OfficeRepository(_context, _mapper);

        public IPhotoRepository PhotoRepository => new PhotoRepository(_context);

        public IVisitRepository VisitRepository => new VisitRepository(_context, _mapper);

        public ISpecialisationRepository SpecialisationRepository => new SpecialisationRepository(_context, _mapper);

        public IDoctorServiceRepository DoctorServiceRepository => new DoctorServiceRepository(_context, _mapper);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}