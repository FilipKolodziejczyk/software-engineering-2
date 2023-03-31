using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.DTO; 

public record SampleDTO() {
    public string Name { get; init; }
    public string Type { get; init; }
}