using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.DTO;

public record NewProductDTO() {
    public string Name { get; init; }
    public string Description { get; init; }
    public string Image { get; init; } //to discuss
    public decimal Price { get; init; }
    public int InStock { get; init; }
}