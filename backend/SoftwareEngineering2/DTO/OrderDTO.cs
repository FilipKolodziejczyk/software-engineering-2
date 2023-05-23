using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.DTO;

public record OrderDTO() {
    public int OrderID { get; init; }
    public int ClientID { get; init; }
    public AddressDTO Address { get; init; }
    public ICollection<OrderedItemDTO> Items { get; init; }
    public string Status { get; init; }
    public UserDTO? DeliveryMan { get; set; }
}
