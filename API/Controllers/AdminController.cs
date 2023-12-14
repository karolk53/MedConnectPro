using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : BaseApiController
    {
        private readonly IMapper _mapper;
        public AdminController(IMapper mapper)
        {
            this._mapper = mapper;
        }

    }
}