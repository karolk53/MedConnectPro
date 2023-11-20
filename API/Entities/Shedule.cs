namespace API.Entities
{
    public class Shedule
    {
        public int Id { get; set; }
        public string WeekDay { get; set; }
        public List<TimeOnly> Hours { get; set; } = new ();
        public int VisitTime { get; set; }
        public int OfficeId { get; set; }
        public Office Office { get; set; }
    }
}