using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class ComplaintModel {
    public int ComplaintId { get; set; }
    
    public string? Topic { get; set; }
    
    public string? Description { get; set; }
    
    public ClientModel? Client { get; set; }

    public OrderModel? Order { get; set; }

    public EmployeeModel? Employee { get; set; }
}
