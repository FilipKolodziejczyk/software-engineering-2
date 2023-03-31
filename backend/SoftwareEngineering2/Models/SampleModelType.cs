using System.ComponentModel.DataAnnotations;

namespace SoftwareEngineering2.Models; 

public class SampleModelType {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}