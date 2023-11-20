namespace API.DTOs
{
    public class SheduleCreateDto
    {
        public string WeekDay { get; set; }
        public string StartHour {get; set;}
        public string EndHour { get; set; }
        public int VisitTime { get; set; }
    }
}