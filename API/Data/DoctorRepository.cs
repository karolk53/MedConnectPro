using API.DTOs;
using API.Entities;
using API.Helpers;
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
            return await _context.Doctors
                            .Include(p => p.Photo)
                            .Include(n => n.Notes)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<DoctorDto> GetDoctorByIdAsync(int id)
        {
            return _context.Doctors
                    .Where(x => x.Id == id)
                    .Include(s => s.DoctorsSpecialisations)
                    .ThenInclude(z => z.Specialisation)
                    .ProjectTo<DoctorDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();
        }

        public async Task<PagedList<DoctorListDto>> GetDoctorsListAsync(DoctorParams doctorParams)
        {
            var query = _context.Doctors.AsQueryable();

            query = query.Where(x => x.DoctorsSpecialisations.Any(x => x.Specialisation.Name == doctorParams.Specialisation));

            return await PagedList<DoctorListDto>.CreateAsync(query.AsNoTracking().ProjectTo<DoctorListDto>(_mapper.ConfigurationProvider), doctorParams.PageNumber, doctorParams.PageSize);
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