using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class OrderDetailsModel {
    public int CurrentOrderId { get; set; }
    
    public int Quantity { get; set; }
    
    public OrderModel? Order { get; set; }
    
    public ProductModel? Product { get; set; }
}
