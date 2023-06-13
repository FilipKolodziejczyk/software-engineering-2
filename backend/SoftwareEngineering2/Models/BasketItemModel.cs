using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class BasketItemModel {
    public int BasketItemID { get; set; }

    [Required]
    public int ClientID { get; set; }
    public ClientModel? Client { get; set; }

    [Required]
    public int ProductID { get; set; }
    public ProductModel? Product { get; set; }

    [Required]
    public int Quantity { get; set; }
}
