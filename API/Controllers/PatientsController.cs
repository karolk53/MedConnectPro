using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Policy = "PatientOnly")]
public class PatientsController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IPatientRepository _patientRepository;
        private readonly IVisitRepository _visitRepository;

    public PatientsController(IPatientRepository patientRepository, IMapper mapper, IVisitRepository visitRepository)
    {
        this._visitRepository = visitRepository;
        this._patientRepository = patientRepository;
        this._mapper = mapper;
    }


    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
    {
        var patients = await _patientRepository.GetAllPatientsAsync();
        return Ok(patients);
    }

    
    [HttpGet("me")]
    public async Task<ActionResult<PatientProfileDto>> GetPatient()
    {
        return await _patientRepository.GetPatientByIdAsync(User.GetUserId());
    }

    [HttpPut("update")]
    public async Task<ActionResult> UpdatePatientsProfile(PatientUpdateDto updateDto)
    {
        var user = await _patientRepository.GetPatientById(User.GetUserId());

        if (user == null) return NotFound();

        if(!Validators.ValidatePesel(updateDto.PESEL)) return BadRequest("Invalid PESEl");

        _mapper.Map(updateDto, user);
        if (await _patientRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Faild to update user!");
    }

    [HttpPut("address/update")]
    public async Task<ActionResult<PatientProfileDto>> UpdatePatientsAddress(AddressDto addressDto)
    {
        var user = await _patientRepository.GetPatientById(User.GetUserId());

        if (user == null) return NotFound();

        _mapper.Map(addressDto, user.Address);
        if (await _patientRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Faild to update address!");
    }

    [HttpGet("visits")]
    public async Task<ActionResult<IEnumerable<Visit>>> GetPatientsVisitsList([FromQuery]VisitParams visitParams)
    {
        var patientId = User.GetUserId();
        var visits = await _visitRepository.GetPatientVisitsListAsync(patientId, visitParams);
        return Ok(visits);
    }



}
