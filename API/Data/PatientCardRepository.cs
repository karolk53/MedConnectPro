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
    public class PatientCardRepository : IPatientCardRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public PatientCardRepository(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            
        }

        public void AddNewCard(PatientCard patientCard)
        {
            _context.PatientCards.Add(patientCard);
        }

        public async Task<IEnumerable<PatientCardList>> GetDoctorCards(int doctorId, CardParams cardParams)
        {
            var query = _context.PatientCards.Where(x => x.DoctorId == doctorId).AsQueryable();
            
            if(!cardParams.PatientName.IsNullOrEmpty())
            {
                query = query.Where(n => string.Concat(n.Patient.FirstName, " ", n.Patient.LastName).Contains(cardParams.PatientName));
            }
            
            if(!cardParams.PatientPESEL.IsNullOrEmpty())
            {
                query = query.Where(n => n.Patient.PESEL == cardParams.PatientPESEL);
            }

            return await PagedList<PatientCardList>.CreateAsync(query.AsNoTracking().ProjectTo<PatientCardList>(_mapper.ConfigurationProvider),  cardParams.PageNumber, cardParams.PageSize);;
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}