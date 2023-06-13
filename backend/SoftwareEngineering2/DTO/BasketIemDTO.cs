using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.DTO;

public record BasketItemDTO() {
    public int ProductID { get; init; }
    public int Quantity { get; init; }
}
