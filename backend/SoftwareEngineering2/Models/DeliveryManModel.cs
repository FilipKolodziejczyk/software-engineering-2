using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class DeliveryManModel : IUserModel {
    public int DeliveryManID { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }

    public ICollection<OrderModel>? Orders { get; }
}
