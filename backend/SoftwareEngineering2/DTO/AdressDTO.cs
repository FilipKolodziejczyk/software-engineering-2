using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.DTO;

public record AddressDTO() {
    public string Street { get; init; }
    public string City { get; init; }
    public string PostalCode { get; init; }
    public string Country { get; init; }
}