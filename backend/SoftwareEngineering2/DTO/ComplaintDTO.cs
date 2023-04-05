using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.DTO;

public record ComplaintDTO() {
    public string Title { get; init; }
    public string Description { get; init; }
    public int? RelatedOrderID { get; init; }
}