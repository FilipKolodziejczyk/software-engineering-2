namespace SoftwareEngineering2.DTO;

public record OrderDto {
    public int OrderId { get; init; }
    public int ClientId { get; init; }
    public AddressDto Address { get; init; }
    public ICollection<OrderedItemDto> Items { get; init; }
    public string Status { get; init; }
    public UserDto? DeliveryMan { get; set; }
}
