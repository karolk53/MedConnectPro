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
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photo.Url));
            CreateMap<DoctorUpdateDto, Doctor>();

            CreateMap<AddressDto, Address>();
            CreateMap<Address,AddressDto>();

            CreateMap<SpecialisationDto, Specialisation>();
            CreateMap<SpecialisationDto, SpecialisationDto>();
            CreateMap<Specialisation, SpecialisationDto>();
            
            CreateMap<Photo, PhotoDto>();
        }
    }
}