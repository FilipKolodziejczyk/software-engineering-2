using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.DTO;

public record ProductDTO() {
    public int ProductID { get; init; }
    
    public string? Name { get; init; }
    
    public decimal Price { get; init; }
    
    public int Quantity { get; init; }
    
    public string? Description { get; init; }

    public string Image { get; init; }
    
    public bool Archived { get; init; }
    
    public string Category { get; init; }
}