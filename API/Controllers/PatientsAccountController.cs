
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    [Route("/api/patients/account")]
    public class PatientsAccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly DataContext _context;

        public PatientsAccountController(DataContext context, ITokenService tokenService)
        {
            this._context = context;
            this._tokenService = tokenService;

        }

        [HttpPost("register")]
        public async Task<ActionResult<PatientDto>> Register(PatientRegisterDto registerDto)
        {

            if (await UserExists(registerDto.Email)) return BadRequest("User with that email already exists!");

            using var hmac = new HMACSHA512();

            var patient = new Patient
            {
                Email = registerDto.Email.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
                UserRole = "Patient",
                Address = new Address { }
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return new PatientDto
            {
                Email = patient.Email,
                Token = _tokenService.CreateToken(patient)
            };

        }

        [HttpPost("login")]
        public async Task<ActionResult<PatientDto>> Login(LoginDto loginDto)
        {
            var patient = await _context.Patients.SingleOrDefaultAsync(x => x.Email.Equals(loginDto.Email.ToLower()));

            if (patient == null) return Unauthorized("Invalid email or password!");

            using var hmac = new HMACSHA512(patient.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != patient.PasswordHash[i]) return Unauthorized("Invalid email or password!");
            }

            return new PatientDto
            {
                Email = patient.Email,
                Token = _tokenService.CreateToken(patient)
            };

        }


        public async Task<bool> UserExists(string email)
        {
            return await _context.Patients.AnyAsync(x => x.Email.Equals(email.ToLower()));
        }

    }
}