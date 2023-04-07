using AutoMapper;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Profiles;

public class AutoMapperProfile : Profile {
    public AutoMapperProfile() {
        CreateMap<SampleModel, SampleDTO>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.Name));
    }
}