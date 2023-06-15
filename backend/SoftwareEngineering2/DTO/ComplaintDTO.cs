namespace SoftwareEngineering2.DTO;

public record ComplaintDto {
    public string Title { get; init; }
    public string Description { get; init; }
    public int? RelatedOrderId { get; init; }
}