using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class OrderDetailsModel {
    public int CurrentOrderId { get; set; }

    [Required]
    public int OrderId { get; set; }
    public OrderModel? Order { get; set; }

    [Required]
    public int ProductId { get; set; }
    public ProductModel? Product { get; set; }

    [Required]
    public int Quantity { get; set; }
}
