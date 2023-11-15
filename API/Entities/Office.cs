namespace API.Entities
{
    public class Office
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public List<Shedule> Shedules { get; set; }
    }
}