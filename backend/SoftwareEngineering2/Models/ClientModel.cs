using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class ClientModel {
  public int ClientID { get; set; }

  [Required]
  public string? FirstName { get; set; }

  [Required]
  public string? LastName { get; set; }

  [Required]
  public string? EmailAddress { get; set; }

  [Required]
  public int AddressID { get; set; }

  [Required]
  public bool HasNewsletterOn { get; set; }

  [Required]
  public string? Username { get; set; }

  [Required]
  public string? Password { get; set; }
}
