using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class OrderDetailsModel {
    public int CurrentOrderID { get; set; }

    [Required]
    public int OrderID { get; set; }
    public OrderModel? Order { get; set; }

    [Required]
    public int ProductID { get; set; }
    public ProductModel? Product { get; set; }

    [Required]
    public int Quantity { get; set; }
}
