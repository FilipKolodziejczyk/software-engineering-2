using AutoMapper;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Services;

public class SampleService : ISampleService {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISampleModelRepository _sampleModelRepository;
    private readonly ISampleModelTypeRepository _sampleModelTypeRepository;
    private readonly IMapper _mapper;

    public SampleService(
        IUnitOfWork unitOfWork,
        ISampleModelRepository sampleModelRepository,
        ISampleModelTypeRepository sampleModelTypeRepository,
        IMapper mapper) {
        _unitOfWork = unitOfWork;
        _sampleModelRepository = sampleModelRepository;
        _sampleModelTypeRepository = sampleModelTypeRepository;
        _mapper = mapper;
    }

    public async Task<SampleModelType?> GetModelTypeByNameAsync(string name) {
        return await _sampleModelTypeRepository.GetByNameAsync(name);
    }

    public async Task<List<SampleDTO>> GetFilteredModelsAsync(string namePattern, string typePattern) {
        var result = await _sampleModelRepository.GetAllFilteredAsync(namePattern, typePattern);
        return new List<SampleDTO>(result.Select(item => _mapper.Map<SampleDTO>(item)));
    }

    public async Task<SampleDTO?> GetModelByIdAsync(int id) {
        var result = await _sampleModelRepository.GetByIdAsync(id);
        return _mapper.Map<SampleDTO>(result);
    }

    public async Task<SampleDTO?> GetModelByData(string name, string type) {
        var result = await _sampleModelRepository.GetByNameAndTypeAsync(name, type);
        return _mapper.Map<SampleDTO>(result);
    }

    public async Task<SampleDTO> CreateModelAsync(NewSampleDTO newSample) {
        var model = _mapper.Map<SampleModel>(newSample);
        model.Type = await _sampleModelTypeRepository.GetByNameAsync(newSample.Type) ?? throw new Exception("Type not found");
        await _sampleModelRepository.AddAsync(model);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<SampleDTO>(model);
    }

    public async Task<SampleDTO> UpdateModelAsync(int id, NewSampleDTO newSample) {
        var model = await _sampleModelRepository.GetByIdAsync(id) ?? throw new Exception("Model not found");
        model.Type = await _sampleModelTypeRepository.GetByNameAsync(newSample.Type) ?? throw new Exception("Type not found");
        model.Name = newSample.Name;
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<SampleDTO>(model);
    }

    public async Task DeleteModelAsync(int id) {
        var model = await _sampleModelRepository.GetByIdAsync(id) ?? throw new Exception("Model not found");
        _sampleModelRepository.Delete(model);
        await _unitOfWork.SaveChangesAsync();
    }
}