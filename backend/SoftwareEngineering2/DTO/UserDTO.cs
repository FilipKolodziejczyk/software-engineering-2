using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.DTO;

public record UserDTO() {
    public int UserID { get; init; }
    public string? Name { get; init; }
    public string? Email { get; init; }
    public string? Role { get; init; }
    public AddressDTO? Address { get; init; }
}