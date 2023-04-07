using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.ModelEntityTypeConfiguration;

namespace SoftwareEngineering2.Models;

[EntityTypeConfiguration(typeof(SampleModelEntityConfiguration))]
public class SampleModel {
    public int Id { get; set; }
    public string Name { get; set; }
    
    public SampleModelType Type { get; set; }
}