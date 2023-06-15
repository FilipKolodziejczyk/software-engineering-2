using System.ComponentModel.DataAnnotations;

namespace SoftwareEngineering2.Models; 

public class ImageModel {
    public int ImageId { get; set; }
    
    public Uri ImageUri { get; set; }
    
    public List<ProductModel> Products { get; set; }
}