using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class SpecialisationRepository : ISpecialisationRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SpecialisationRepository(DataContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
            
        }

        public void AddSpecialisation(Specialisation specialisation)
        {
            _context.Specialisations.Add(specialisation);
        }

        public void DeleteSpecialisation(Specialisation specialisation)
        {
            _context.Specialisations.Remove(specialisation);
        }

        public async Task<IEnumerable<SpecialisationDto>> GetDoctorsSpecialisations(int doctorId) //MOŻE PRZENIEŚĆ DO INNEGO REPO????
        {
            
            return await  _context.DoctorsSpecialisations
                    .Where(x => x.DoctorId == doctorId)
                    .Select(spec => spec.Specialisation)
                    .Select(spec => new SpecialisationDto
                    {
                        Id = spec.Id,
                        Name = spec.Name
                    })
                    .ProjectTo<SpecialisationDto>(_mapper.ConfigurationProvider)
                    .ToArrayAsync();
        }

        public async Task<Specialisation> GetSpecialisationById(int id)
        {
            return await _context.Specialisations.FindAsync(id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}