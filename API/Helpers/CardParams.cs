namespace API.Helpers
{
    public class CardParams : PaginationParams
    {
        public string PatientName { get; set; }
        public string PatientPESEL { get; set; }
    }
}