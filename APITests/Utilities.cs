using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;

namespace APITests
{
    public static class Utilities
    {
        public static MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }

        public static async Task InitializeDbForTests(
            DataContext context
        )
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            using var hmac = new HMACSHA512();

            var patient = new Patient
            {
                Email = "jan@example.com",
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd")),
                PasswordSalt = hmac.Key,
                UserRole = "Patient",
                Address = new Address { }
            };

            context.Patients.Add(patient);
            await context.SaveChangesAsync();

            var doctor = new Doctor
            {
                Email = "doctor@doctor.com",
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd")),
                PasswordSalt = hmac.Key,
                UserRole = "Doctor",
                PWZ = "7206452"
            };

            context.Doctors.Add(doctor);
            await context.SaveChangesAsync();

            var note = new Note
            {
                Description = "Note",
                Value = 3,
                Doctor = doctor,
                Patient = patient
            };

            context.Notes.Add(note);
            await context.SaveChangesAsync();

            var office = new Office
            {
                Name = "Testing office",
                Address = new Address
                {
                    Street = "Zielona",
                    BuildingNumber = "10",
                    PostCode = "21-222",
                    City = "Lublin"
                },
                Shedules = new List<Shedule> 
                {
                    new Shedule
                    {
                        WeekDay = "Friday",
                        Hours = { new TimeOnly(14,30,0), new TimeOnly(15,0,0), new TimeOnly(15,30,0)},
                        VisitTime = 30
                    }
                }
            };

            context.Offices.Add(office);
            doctor.Office = office;
            await context.SaveChangesAsync();

            return;
        }
    }
}