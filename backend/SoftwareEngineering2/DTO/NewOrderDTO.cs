using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.DTO;

public record NewOrderDTO() {
    public AddressDTO Address { get; init; }
    public ICollection<OrderedItemDTO> Items { get; init; }
}
