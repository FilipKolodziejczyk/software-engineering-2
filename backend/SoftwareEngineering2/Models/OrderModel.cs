namespace SoftwareEngineering2.Models;

public class OrderModel {
    public int OrderId { get; set; }

    public string? Status { get; set; }

    public ClientModel? Client { get; set; }
    public DeliveryManModel? DeliveryMan { get; set; }
    public AddressModel? Address { get; set; }
    public ICollection<OrderDetailsModel> OrderDetails { get; }
    public ICollection<ComplaintModel> Complaints { get; }
}