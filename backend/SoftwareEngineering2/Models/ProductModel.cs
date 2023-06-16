using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class ProductModel {
    public int ProductId { get; set; }
    
    public string Name { get; set; } = null!;
    
    public decimal Price { get; set; }
    
    public int Quantity { get; set; }
    
    public string? Description { get; set; }
    
    public bool Archived { get; set; }

    public string Category { get; set; } = null!;
    
    public List<ImageModel> Images { get; set; }

    public ICollection<OrderDetailsModel> OrderDetails { get; set; }
    public ICollection<BasketItemModel> BasketItems { get; set; }
}
