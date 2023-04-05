using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.DTO;

public record OrderDTO() {
    public int ClientID { get; init; }
    public AddressDTO Address { get; init; }
    public ICollection<OrderedItemDTO> Items { get; init; }
}
