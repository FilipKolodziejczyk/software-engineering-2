using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class ProductModel {
    public int ProductID { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public bool Archived { get; set; }

    public string Category { get; set; }
    
    public List<ImageModel> Images { get; set; }

    public ICollection<OrderDetailsModel>? OrderDetails { get; set; }
    public ICollection<BasketItemModel>? BasketItems { get; set; }
}
