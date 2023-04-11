using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class ComplaintModel {
    public int ComplaintID { get; set; }

    [Required]
    public int ClientID { get; set; }
    public ClientModel? Client { get; set; }

    [Required]
    public int OrderID { get; set; }
    public OrderModel? Order { get; set; }

    [Required]
    public int EmployeeID { get; set; }
    public EmployeeModel? Employee { get; set; }

    [Required]
    public string? Topic { get; set; }

    [Required]
    public string? Description { get; set; }
}
