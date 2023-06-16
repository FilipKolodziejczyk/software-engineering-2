using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class BasketItemModel {
    public int BasketItemId { get; set; }
    
    public ClientModel? Client { get; set; }
    
    public ProductModel? Product { get; set; }
    
    public int Quantity { get; set; }
}
