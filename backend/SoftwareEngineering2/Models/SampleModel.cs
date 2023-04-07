using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareEngineering2.Models;

public class SampleModel {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    [Column(TypeName = "VARCHAR(255)")]
    [Required]
    public string Name { get; set; }
    
    [ForeignKey("SampleModelTypeId")]
    public SampleModelType Type { get; set; }
}