using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class ClientModel : IUserModel {
    public int ClientID { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public int AddressID { get; set; }
    public AddressModel? Address { get; set; }

    [Required]
    public bool HasNewsletterOn { get; set; }

    [Required]
    public string? Password { get; set; }

    public ICollection<ComplaintModel>? Complaints { get; }
    public ICollection<OrderModel>? Orders { get; }
    public ICollection<BasketItemModel>? BasketItems { get; }
}
