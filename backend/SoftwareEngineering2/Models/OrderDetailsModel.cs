using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class OrderDetailsModel {
  public int CurrentOrderID { get; set; }

  [Required]
  public int OrderID { get; set; }

  [Required]
  public int ProductID { get; set; }

  [Required]
  public int Quantity { get; set; }
}
