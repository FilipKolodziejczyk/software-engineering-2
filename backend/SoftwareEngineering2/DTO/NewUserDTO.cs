namespace SoftwareEngineering2.DTO;

public record NewUserDto {
    public string? Name { get; init; }
    public string? Email { get; init; }
    public string? Password { get; init; }
    public bool Newsletter { get; init; }
    public string? Role { get; init; }
    public AddressDto? Address { get; init; }
}