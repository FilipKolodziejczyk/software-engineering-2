namespace SoftwareEngineering2.DTO;

public record NewImageDto {
    public IFormFile Image { get; init; } = null!;
}