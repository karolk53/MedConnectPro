using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<Patient, PatientDto>();
            CreateMap<Patient, PatientProfileDto>();
            CreateMap<PatientUpdateDto, Patient>();

            CreateMap<Doctor, DoctorListDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photo.Url));
            CreateMap<Doctor, DoctorDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photo.Url))
                .ForMember(dest => dest.NotesCount, opt => opt.MapFrom(src => src.Notes.Count()));
            CreateMap<DoctorUpdateDto, Doctor>();

            CreateMap<AddressDto, Address>();
            CreateMap<Address, AddressDto>();

            CreateMap<SpecialisationDto, Specialisation>();
            CreateMap<SpecialisationDto, SpecialisationDto>();
            CreateMap<Specialisation, SpecialisationDto>();
            
            CreateMap<Photo, PhotoDto>();

            CreateMap<Note, NoteDto>();
            CreateMap<NoteDto, Note>();

            CreateMap<DoctorService, DoctorServiceDto>();
            CreateMap<DoctorServiceUpdateDto, DoctorService>();

            CreateMap<Shedule, SheduleDto>();

            CreateMap<Office, OfficeCreateDto>();
            CreateMap<Office, OfficeDto>()
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.FlatNumber, opt => opt.MapFrom(src => src.Address.FlatNumber))
                .ForMember(dest => dest.PostCode, opt => opt.MapFrom(src => src.Address.PostCode))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));
            CreateMap<OfficeCreateDto, Office>();

            CreateMap<Visit, VisitDto>()
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => string.Join(" ", src.Doctor.FirstName, src.Doctor.LastName)))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => string.Join(" ", src.Patient.FirstName, src.Patient.LastName)));
        }
    }
}