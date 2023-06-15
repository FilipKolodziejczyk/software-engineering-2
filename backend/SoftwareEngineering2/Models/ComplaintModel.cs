using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class ComplaintModel {
    public int ComplaintId { get; set; }

    [Required]
    public int ClientId { get; set; }
    public ClientModel? Client { get; set; }

    [Required]
    public int OrderId { get; set; }
    public OrderModel? Order { get; set; }

    [Required]
    public int EmployeeId { get; set; }
    public EmployeeModel? Employee { get; set; }

    [Required]
    public string? Topic { get; set; }

    [Required]
    public string? Description { get; set; }
}
