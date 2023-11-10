using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PatientRepository : IPatientRepository
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public PatientRepository(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            
        }

        public async Task<IEnumerable<PatientProfileDto>> GetAllPatientsAsync()
        {
            return await _context.Patients
                        .ProjectTo<PatientProfileDto>(_mapper.ConfigurationProvider)
                        .ToListAsync();
        }

        public async Task<Patient> GetPatientById(int id)
        {
            return await _context.Patients.Include(a => a.Address).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PatientProfileDto> GetPatientByIdAsync(int id)
        {
            return await _context.Patients
                        .Where(x => x.Id == id)
                        .ProjectTo<PatientProfileDto>(_mapper.ConfigurationProvider)
                        .SingleOrDefaultAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Patient patient)
        {
            throw new NotImplementedException();
        }
    }
}