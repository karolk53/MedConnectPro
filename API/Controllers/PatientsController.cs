﻿using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PatientsController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IPatientRepository _patientRepository;

    public PatientsController(IPatientRepository patientRepository, IMapper mapper)
    {
        this._patientRepository = patientRepository;
        this._mapper = mapper;
    }


    //[Authorize(Roles ="Admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatients(){
        var patients = await _patientRepository.GetAllPatientsAsync();
        return Ok(patients);
    }

    //[Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<PatientProfileDto>> GetPatient(int id){
        return await _patientRepository.GetPatientById(id);
    }   

}
