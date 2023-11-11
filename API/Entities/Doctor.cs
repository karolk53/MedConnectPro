namespace API.Entities;

public class Doctor : User
{
    public string PWZ { get; set; }
    public List<DoctorSpecialisation> DoctorsSpecialisations  { get; set; }
    public int? PhotoId { get; set; }
    public Photo Photo { get; set; }
}
