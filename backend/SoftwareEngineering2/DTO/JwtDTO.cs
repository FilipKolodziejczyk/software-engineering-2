using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.DTO;

public record JwtDTO() {
    public string? Jwttoken { get; init; }
}