using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DoctorServiceRepository : IDoctorServiceRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public DoctorServiceRepository(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            
        }

        public void CreateNewService(DoctorService doctorService)
        {
            _context.DoctorServices.Add(doctorService);
        }

        public void DeleteService(DoctorService doctorService)
        {
            _context.DoctorServices.Remove(doctorService);
        }

        public async Task<DoctorService> GetDoctorService(int serviceId)
        {
            return await _context.DoctorServices.FindAsync(serviceId);
        }

        public async Task<IEnumerable<DoctorServiceDto>> GetDoctorServices(int doctorId)
        {
            return await _context.DoctorServices
                                    .Where(x => x.DoctorId == doctorId)
                                    .ProjectTo<DoctorServiceDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}