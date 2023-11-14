namespace API.Entities
{
    public class DoctorService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descripton { get; set; }
        public double Price { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}