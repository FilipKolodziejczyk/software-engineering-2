using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.ModelEntityTypeConfiguration;

namespace SoftwareEngineering2.Models; 

[EntityTypeConfiguration(typeof(SampleModelTypeEntityConfiguration))]
public class SampleModelType {
    public int Id { get; set; }
    public string Name { get; set; }
    public IReadOnlyCollection<SampleModel> Models { get; private set; }
}