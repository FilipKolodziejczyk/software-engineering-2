namespace SoftwareEngineering2.DTO;

public record OrderedItemDto {
    public int ProductId { get; init; }
    public int Quantity { get; init; }
}
