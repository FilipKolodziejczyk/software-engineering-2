namespace SoftwareEngineering2.DTO;

public record ImageDTO() {
    public int ImageId { get; init; }
    
    public Uri ImageUri { get; init; }
}