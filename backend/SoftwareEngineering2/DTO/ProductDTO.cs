using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.DTO;

public record ProductDTO() {
    public string Name { get; init; }
    public string Description { get; init; }
    public byte[] Image { get; init; }
}