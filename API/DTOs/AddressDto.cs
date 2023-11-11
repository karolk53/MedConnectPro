namespace API.DTOs
{
    public class AddressDto
    {
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string FlatNumber { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
    }
}