using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.DTO;

public record NewsletterDTO() {
    public bool Subscribed { get; init; }
}