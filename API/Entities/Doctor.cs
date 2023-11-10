namespace API.Entities;

public class Doctor : User
{
    public string PWZ { get; set; }
    public List<DoctorSpecialisation> DoctorsSpecialisations  { get; set; }

}
