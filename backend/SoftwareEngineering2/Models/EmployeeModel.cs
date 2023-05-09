using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class EmployeeModel : IUserModel {
    public int EmployeeID { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }

    public ICollection<ComplaintModel>? Complaints { get; }
}
