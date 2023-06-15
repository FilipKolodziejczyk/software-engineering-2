namespace SoftwareEngineering2.DTO;

public record UserDto {
    public int UserId { get; init; }
    public string? Name { get; init; }
    public string? Email { get; init; }
    public string? Role { get; init; }
    public bool? Newsletter { get; init; }
    public AddressDto? Address { get; init; }
}