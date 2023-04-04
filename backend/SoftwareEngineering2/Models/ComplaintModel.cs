using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class ComplaintModel {
  public int ComplaintID { get; set; }

  [Required]
  public int ClientID { get; set; }

  [Required]
  public int OrderID { get; set; }

  [Required]
  public int EmployeeID { get; set; }

  [Required]
  public string? Topic { get; set; }

  [Required]
  public string? Description { get; set; }
}
