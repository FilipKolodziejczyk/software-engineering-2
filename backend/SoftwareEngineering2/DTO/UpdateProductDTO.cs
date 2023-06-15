using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.DTO;

public record UpdateProductDTO() {
    public int ProductID { get; init; }
    
    public string Name { get; init; }
    
    public string Description { get; init; }
    
    public List<int> ImageIds { get; init; }
    
    public decimal Price { get; init; }
    
    public int Quantity { get; init; }
    
    public string Category{ get; init; }
    
    public bool Archived { get; set; }
}