using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class OrderModel {
    public int OrderId { get; set; }

    [Required]
    public int ClientId { get; set; }
    public ClientModel? Client { get; set; }

    public int? DeliveryManId { get; set; }
    public DeliveryManModel? DeliveryMan { get; set; }

    [Required]
    public int AddressId { get; set; }
    public AddressModel? Address { get; set; }

    [Required]
    public string? Status { get; set; }

    public ICollection<OrderDetailsModel>? OrderDetails { get; }
    public ICollection<ComplaintModel>? Complaints { get; }
}
