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
    public DbSet<Address> Addresses {get; set;}
    public DbSet<Specialisation> Specialisations { get; set; }
    public DbSet<DoctorSpecialisation> DoctorsSpecialisations  { get; set; }
    public DbSet<Photo> Photos {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DoctorSpecialisation>().HasKey(k => new {k.DoctorId, k.SpecialisationId});

        modelBuilder.Entity<DoctorSpecialisation>()
            .HasOne<Doctor>(d => d.Doctor)
            .WithMany(s => s.DoctorsSpecialisations)
            .HasForeignKey(d => d.DoctorId);

        modelBuilder.Entity<DoctorSpecialisation>()
            .HasOne<Specialisation>(d => d.Specialisation)
            .WithMany(s => s.DoctorsSpecialisations)
            .HasForeignKey(d => d.SpecialisationId);

        modelBuilder.Entity<Photo>()
            .HasOne<Doctor>(d => d.Doctor)
            .WithOne(x => x.Photo)
            .HasForeignKey<Doctor>(f => f.PhotoId)
            .OnDelete(DeleteBehavior.Cascade);

    }

}
