using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class AddressDto
    {
        [Required]
        public string Street { get; set; }
        [Required]
        public string BuildingNumber { get; set; }
        [RegularExpression("[1-9]{1,3}")]
        public string FlatNumber { get; set; }
        [Required]
        [RegularExpression("[0-9]{2}-[0-9]{3}")]
        public string PostCode { get; set; }
        [Required]
        public string City { get; set; }
    }
}