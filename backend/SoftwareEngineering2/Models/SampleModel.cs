using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class SampleModel {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    
    [Required]
    public SampleModelType Type { get; set; }
}