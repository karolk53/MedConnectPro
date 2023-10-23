using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : DbContext
{

    public DataContext(DbContextOptions options): base(options)
    {
        
    }

    public DbSet<Patient> Patients {get; set;}

    public DbSet<Doctor> Doctors {get; set;}

}
