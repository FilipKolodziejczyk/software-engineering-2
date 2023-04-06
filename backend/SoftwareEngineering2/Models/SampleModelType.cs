using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareEngineering2.Models; 

[Table("SampleModelTypes")]
public class SampleModelType {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    [Column(TypeName = "VARCHAR(255)")]
    [Required]
    public string Name { get; set; }
}