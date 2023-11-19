namespace API.Helpers
{
    public class VisitParams : PaginationParams
    {
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Date { get; set; }
    }
}