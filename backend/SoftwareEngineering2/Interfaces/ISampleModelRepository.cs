using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Interfaces; 

public interface ISampleModelRepository {
    Task<IEnumerable<SampleModel>> GetAllFilteredAsync(string? name, string? type, bool exactMatch = false);
    Task<SampleModel?> GetByIdAsync(int id);
    Task AddAsync(SampleModel sample);
    void Delete(SampleModel sample);
}