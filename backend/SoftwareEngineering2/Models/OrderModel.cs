using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class OrderModel {
    public int OrderID { get; set; }

    [Required]
    public int ClientID { get; set; }
    public ClientModel? Client { get; set; }

    public int? DeliveryManID { get; set; }
    public DeliveryManModel? DeliveryMan { get; set; }

    [Required]
    public int AddressID { get; set; }
    public AddressModel? Address { get; set; }

    [Required]
    public string? Status { get; set; }

    public ICollection<OrderDetailsModel>? OrderDetails { get; }
    public ICollection<ComplaintModel>? Complaints { get; }
}
