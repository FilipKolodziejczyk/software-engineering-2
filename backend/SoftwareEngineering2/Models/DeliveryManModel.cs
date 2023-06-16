using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class DeliveryManModel : IUserModel {
    public int DeliveryManId { get; set; }
    
    public string? Name { get; set; }
    
    public string? Email { get; set; }
    
    public string? Password { get; set; }

    public ICollection<OrderModel>? Orders { get; }
}
