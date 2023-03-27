using System.ComponentModel.DataAnnotations;

namespace SoftwareEnginnering2.Models; 

public class SampleModelType {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}