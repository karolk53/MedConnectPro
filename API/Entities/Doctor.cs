namespace API.Entities;

public class Doctor : User
{
    public string PWZ { get; set; }
    public List<DoctorSpecialisation> DoctorsSpecialisations { get; set; }
    public int? PhotoId { get; set; }
    public Photo Photo { get; set; }
    public List<Note> Notes { get; set; }
    public double TotalRating { get; set; }
    public List<DoctorService> DoctorServices { get; set; }
    public int? OfficeId { get; set; }
    public Office Office { get; set; }
    public List<Visit> Visits { get; set; }
}
