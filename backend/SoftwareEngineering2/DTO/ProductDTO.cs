namespace SoftwareEngineering2.DTO;

public record ProductDto {
    public int ProductId { get; init; }

    public string? Name { get; init; }

    public decimal Price { get; init; }

    public int Quantity { get; init; }

    public string? Description { get; init; }

    public List<int> ImageIds { get; init; }

    public List<Uri> ImageUris { get; init; }

    public bool Archived { get; init; }

    public string Category { get; init; }
}