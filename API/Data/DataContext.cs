using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
    public DbSet<Note> Notes { get; set; }
    public DbSet<DoctorService> DoctorServices { get; set; }
    public DbSet<Shedule> Shedules { get; set; }
    public DbSet<Office> Offices { get; set; }
    public DbSet<Visit> Visits { get; set; }

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
        
        modelBuilder.Entity<Shedule>()
        .Property(x => x.Hours)
        .HasConversion(new ValueConverter<List<TimeOnly>, string>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<List<TimeOnly>>(v, (JsonSerializerOptions)null)));

        var vc = new ValueComparer<List<TimeOnly>>(
                (c1,c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a,v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList());
        
        modelBuilder
            .Entity<Shedule>()
            .Property(h => h.Hours)
            .Metadata
            .SetValueComparer(vc);

        modelBuilder.Entity<Visit>()
            .HasOne(d => d.Doctor)
            .WithMany(v => v.Visits)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Visit>()
            .HasOne(d => d.Patient)
            .WithMany(v => v.Visits)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Visit>()
            .Property(p => p.Status)
            .HasConversion<string>();

    }

}
