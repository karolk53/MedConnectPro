

using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("/api/doctors/account")]
    public class DoctorsAccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly DataContext _context;

        public DoctorsAccountController(DataContext context, ITokenService tokenService)
        {
            this._context = context;
            this._tokenService = tokenService;

        }

        [HttpPost("register")]
        public async Task<ActionResult<DoctorDto>> Register(DoctorRegisterDto registerDto)
        {


            if (await UserExists(registerDto.Email)) return BadRequest("User with that email already exists!");
            if (await PWZTaken(registerDto.PWZ)) return BadRequest("Already taken PWZ");
            if (!Validators.ValidatePWZ(registerDto.PWZ)) return BadRequest("Invalid PWZ");

            using var hmac = new HMACSHA512();

            var doctor = new Doctor
            {
                Email = registerDto.Email.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
                PWZ = registerDto.PWZ.ToLower(),
                UserRole = "Doctor"
            };

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return new DoctorDto
            {
                Email = doctor.Email,
                Token = _tokenService.CreateToken(doctor)
            };

        }

        [HttpPost("login")]
        public async Task<ActionResult<DoctorDto>> Login(LoginDto loginDto)
        {
            var doctor = await _context.Doctors.SingleOrDefaultAsync(x => x.Email.Equals(loginDto.Email.ToLower()));

            if (doctor == null) return Unauthorized("Invalid email or password!");

            using var hmac = new HMACSHA512(doctor.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != doctor.PasswordHash[i]) return Unauthorized("Invalid email or password!");
            }

            return new DoctorDto
            {
                Email = doctor.Email,
                Token = _tokenService.CreateToken(doctor)
            };

        }


        public async Task<bool> UserExists(string email)
        {
            return await _context.Doctors.AnyAsync(x => x.Email.Equals(email.ToLower()));
        }

        public async Task<bool> PWZTaken(string pwz)
        {
            return await _context.Doctors.AnyAsync(x => x.PWZ == pwz.ToLower());
        }

    }
}