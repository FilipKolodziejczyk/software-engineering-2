using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class OrderModel {
  public int OrderID { get; set; }

  [Required]
  public int ClientID { get; set; }

  public int DeliveryManID { get; set; }

  [Required]
  public int AddressID { get; set; }

  [Required]
  public string? Status { get; set; }
}
