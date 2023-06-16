using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class EmployeeModel : IUserModel {
    public int EmployeeId { get; set; }
    
    public string? Name { get; set; }
    
    public string? Email { get; set; }
    
    public string? Password { get; set; }

    public ICollection<ComplaintModel>? Complaints { get; }
}
