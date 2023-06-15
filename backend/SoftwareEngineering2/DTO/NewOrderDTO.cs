namespace SoftwareEngineering2.DTO;

public record NewOrderDto {
    public AddressDto? Address { get; init; }
    public ICollection<OrderedItemDto>? Items { get; init; }
}
