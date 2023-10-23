using System.ComponentModel.DataAnnotations;

namespace API;

public class Patient
{

    public int Id {get; set;}

    public string firstName {get; set;}

    public string lastName { get; set; }

    [Phone]
    public string phone {get; set;}

}
