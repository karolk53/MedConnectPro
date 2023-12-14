namespace API.Helpers
{
    public class VisitParams : PaginationParams
    {
        public string Status { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
    }
}