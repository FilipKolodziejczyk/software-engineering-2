using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Interfaces; 

public interface ISampleModelRepository {
    Task<IEnumerable<SampleModel>> GetAllFilteredAsync(string name, string type);
    Task<SampleModel?> GetByIdAsync(int id);
    Task<SampleModel?> GetByNameAndTypeAsync(string name, string type);
    Task AddAsync(SampleModel sample);
    void Delete(SampleModel sample);
}