using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        public async Task<PagedList<VisitDto>> GetPatientVisitsListAsync(int patientId, VisitParams visitParams)
        {
            var query = _context.Visits.Where(x => x.PatientId == patientId).AsQueryable();

            query = visitParams.Status switch
            {
                "cancelled" => query.Where(s => s.Status == VisitStatus.CANCELED),
                "lasting" => query.Where(s => s.Status == VisitStatus.LAST),
                "completed" => query.Where(s => s.Status == VisitStatus.COMPLETED),
                _ => query.Where(s => s.Status == VisitStatus.PLANNED)
            };

            if(!visitParams.Date.IsNullOrEmpty())
            {
                var date = DateTime.Parse(visitParams.Date);
                query = query.Where(d => d.PlannedDate.Date == date.Date);
            }

            if(!visitParams.Name.IsNullOrEmpty())
            {
                query = query.Where(x => string.Concat(x.Doctor.FirstName, " " ,x.Doctor.LastName).Contains(visitParams.Name));
            }

            return await PagedList<VisitDto>.CreateAsync(query.AsNoTracking().ProjectTo<VisitDto>(_mapper.ConfigurationProvider), visitParams.PageNumber, visitParams.PageSize);
        }

        public async Task<List<VisitPlannedDto>> GetPlannedVisits(int doctorId, string startDate, string endDate)
        {
            return await _context.Visits
                    .Where(x => x.DoctorId == doctorId)
                    .Where(x => x.Status == VisitStatus.PLANNED)
                    .Where(x => 
                        x.PlannedDate >= DateTime.Parse(startDate) && 
                        x.PlannedDate <= DateTime.Parse(endDate)
                        )
                    .ProjectTo<VisitPlannedDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
        }

        public async Task<Visit> GetVisitById(int visitId)
        {
            return await _context.Visits
                                    .Include(p => p.Patient)
                                    .Include(d => d.Doctor)
                                    .FirstOrDefaultAsync(x => x.Id == visitId);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}