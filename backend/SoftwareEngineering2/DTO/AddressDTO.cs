namespace SoftwareEngineering2.DTO;

public record AddressDto {
    public string Street { get; init; }
    public string City { get; init; }
    public string BuildingNo { get; init; }
    public string HouseNo { get; init; }
    public string PostalCode { get; init; }
    public string Country { get; init; }
}