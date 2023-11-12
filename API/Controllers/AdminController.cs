using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly ISpecialisationRepository _specialisationRepository;
        private readonly IMapper _mapper;
        public AdminController(ISpecialisationRepository specialisationRepository, IMapper mapper)
        {
            this._mapper = mapper;
            this._specialisationRepository = specialisationRepository;
            
        }

    }
}