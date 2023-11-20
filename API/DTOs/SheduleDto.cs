namespace API.DTOs
{
    public class SheduleDto
    {
        public string WeekDay { get; set; }
        public List<TimeOnly> Hours { get; set; }
        public int VisitTime { get; set; }
    }
}