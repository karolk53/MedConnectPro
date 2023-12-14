using API.Entities;

namespace API.DTOs
{
    public class OfficeDto
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string FlatNumber { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public List<SheduleDto> Shedules { get; set; }
    }
}