using AutoMapper;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Profiles;

public class AutoMapperProfile : Profile {
    public AutoMapperProfile() {
        CreateMap<SampleModel, SampleDTO>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.Name));
        
        CreateMap<NewSampleDTO, SampleModel>()
            .ForMember(dest => dest.Type, opt => opt.Ignore());

        CreateMap<NewProductDTO, ProductModel>()
            .ForMember(dest => dest.Archived, opt => opt.MapFrom(src => false));
        CreateMap<ProductModel, ProductDTO>(); //Modification may be needed later to handle getting products from database
    }
    
}

