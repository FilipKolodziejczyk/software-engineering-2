using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class DeliveryManModel {
  public int DeliveryManID { get; set; }

  [Required]
  public string? FirstName { get; set; }

  [Required]
  public string? LastName { get; set; }

  [Required]
  public string? Username { get; set; }

  [Required]
  public string? Password { get; set; }
}
