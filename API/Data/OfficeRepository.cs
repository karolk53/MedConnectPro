using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class OfficeRepository : IOfficeRepository
    {   
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public OfficeRepository(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            
        }

        public void AddNewOffice(Office office)
        {
            _context.Offices.Add(office);
        }

        public void DeleteOffice(Office office)
        {
            _context.Offices.Remove(office);
        }

        public Task<OfficeCreateDto> GetDoctorOffice(int doctorId)
        {
            throw new NotImplementedException();
        }

        public async Task<Office> GetOfficeById(int officeId)
        {
            return await _context.Offices.Include(a => a.Address).Include(s => s.Shedules).FirstOrDefaultAsync(x => x.Id == officeId);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}