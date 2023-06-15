using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class BasketItemModel {
    public int BasketItemId { get; set; }

    [Required]
    public int ClientId { get; set; }
    public ClientModel? Client { get; set; }

    [Required]
    public int ProductId { get; set; }
    public ProductModel? Product { get; set; }

    [Required]
    public int Quantity { get; set; }
}
