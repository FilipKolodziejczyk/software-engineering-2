namespace SoftwareEngineering2.DTO; 

public record SampleDTO() {
    public int Id { get; init; }
    public string Name { get; init; }
    public string Type { get; init; }
}