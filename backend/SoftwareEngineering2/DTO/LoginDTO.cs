using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.DTO;

public record LoginDTO() {
    public string Username { get; init; }
    public string Password { get; init; }
}