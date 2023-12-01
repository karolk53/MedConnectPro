namespace API.Helpers
{
    public class DoctorParams : PaginationParams
    {
        public string Specialisation { get; set; }
        public string SortByTotalRating {get; set;}
        public string City { get; set; }
    }
}