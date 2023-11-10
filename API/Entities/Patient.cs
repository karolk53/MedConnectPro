using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class Patient : User
{
    public Address Address { get; set; }

    public string PESEL { get; set; }
}
