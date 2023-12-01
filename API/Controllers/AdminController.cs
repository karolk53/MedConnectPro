using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize(Policy = "AdminOnly")]
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