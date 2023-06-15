namespace SoftwareEngineering2.Models; 

public class ImageModel {
    public int ImageId { get; set; }

    public Uri ImageUri { get; set; } = null!;
    
    public List<ProductModel> Products { get; set; } = null!;
}