using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Interfaces; 

public interface ISampleModelTypeRepository {
    Task<IEnumerable<SampleModelType>> GetAllAsync();
}