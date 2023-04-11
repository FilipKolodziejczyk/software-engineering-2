using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Interfaces; 

public interface ISampleService {
    Task<SampleModelType?> GetModelTypeByNameAsync(string name);
    Task<List<SampleDTO>> GetFilteredModelsAsync(string namePattern, string typePattern);
    Task<SampleDTO?> GetModelByIdAsync(int id);
    Task<SampleDTO?> GetModelByData(string name, string type);
    Task<SampleDTO> CreateModelAsync(NewSampleDTO newSample);
    Task<SampleDTO> UpdateModelAsync(int id, NewSampleDTO newSample);
    Task DeleteModelAsync(int id);
}