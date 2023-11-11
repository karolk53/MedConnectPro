using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DoctorRepository(DataContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
            
        }

        public async Task<Doctor> GetDoctorById(int id)
        {
            return await _context.Doctors.Include(p => p.Photo).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<DoctorDto> GetDoctorByIdAsync(int id)
        {
            return _context.Doctors
                    .Where(x => x.Id == id)
                    .ProjectTo<DoctorDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<DoctorListDto>> GetDoctorsListAsync()
        {
            var doctors = await _context.Doctors
                                        .Include(s => s.DoctorsSpecialisations).ThenInclude(x => x.Specialisation)
                                        .ToListAsync();

            return _mapper.Map<IEnumerable<DoctorListDto>>(doctors);
        }

        public Task<Doctor> GetDoctorWithSpecialisation(int id)
        {
            return _context.Doctors.Include(s => s.DoctorsSpecialisations).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}