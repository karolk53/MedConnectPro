using API.Data;
using API.Entities;
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


    [HttpGet("patients")]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatients(){
        var patients = await _context.Patients.ToListAsync();
        return patients;
    }




}
