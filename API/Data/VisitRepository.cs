using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class VisitRepository : IVisitRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public VisitRepository(DataContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public void AddNewVisit(Visit visit)
        {
            _context.Visits.Add(visit);
        }

        public async Task<IEnumerable<VisitDto>> GetDoctorVisitsList(int doctorId)
        {
             return await _context.Visits.Where(x => x.DoctorId == doctorId).ProjectTo<VisitDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<VisitDto>> GetPatientVisitsList(int patientId)
        {
            return await _context.Visits.Where(x => x.PatientId == patientId).ProjectTo<VisitDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<Visit> GetVisitById(int visitId)
        {
            return await _context.Visits.FindAsync(visitId);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}