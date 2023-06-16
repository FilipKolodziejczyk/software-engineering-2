using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class ClientModel : IUserModel {
    public int ClientId { get; set; }
    
    public string? Name { get; set; }
    
    public string? Email { get; set; }

    public bool HasNewsletterOn { get; set; }
    
    public string? Password { get; set; }
    
    public int AddressId { get; set; } // Needed for seeding
    public AddressModel? Address { get; set; }
    public ICollection<ComplaintModel>? Complaints { get; }
    public ICollection<OrderModel>? Orders { get; }
    public ICollection<BasketItemModel>? BasketItems { get; }
}
