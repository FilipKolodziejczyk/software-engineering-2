namespace SoftwareEngineering2.DTO;

public record ImageDto {
    public int ImageId { get; init; }

    public Uri ImageUri { get; init; } = null!;
}