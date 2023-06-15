namespace SoftwareEngineering2.DTO;

public record NewImageDTO() {
    public IFormFile Image { get; init; }
};