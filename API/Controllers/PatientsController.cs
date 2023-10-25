using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class PatientsController : BaseApiController
{   

    private readonly DataContext _context;

    public PatientsController(DataContext context)
    {
        this._context = context;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatients(){
        var patients = await _context.Patients.ToListAsync();
        return patients;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> GetPatient(int id){
        return await _context.Patients.FindAsync(id);
    }

}
