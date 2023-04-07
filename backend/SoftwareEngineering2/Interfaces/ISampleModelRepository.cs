using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Interfaces; 

public interface ISampleModelRepository {
    Task<IEnumerable<SampleModel>> GetAllAsync();
    Task<SampleModel?> GetByIdAsync(int id);
    Task<SampleModel?> GetByNameAsync(string name);
    Task AddAsync(SampleModel sample);
    void Delete(SampleModel sample);
}