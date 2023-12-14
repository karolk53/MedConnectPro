namespace API.DTOs
{
    public class OfficeCreateDto
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string FlatNumber { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
    }
}