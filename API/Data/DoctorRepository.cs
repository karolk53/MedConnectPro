using API.DTOs;
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

        public async Task<IEnumerable<DoctorListDto>> GetDoctorsListAsync()
        {
            var doctors = await _context.Doctors
                                        .ProjectTo<DoctorListDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
            return doctors;
        }
    }
}