using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class Patient : User
{
    public Address Address { get; set; }
    public string PESEL { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public List<Note> Notes { get; set; }
}
