using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareEngineering2.Models; 

public class SampleModelType {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    [Column(TypeName = "VARCHAR")]
    [MaxLength(50)]
    [Required]
    public string Name { get; set; }
}