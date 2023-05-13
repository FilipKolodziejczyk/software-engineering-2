using AutoMapper;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Profiles;

public class AutoMapperProfile : Profile {
    public AutoMapperProfile() {
        CreateMap<AddressDTO, AddressModel>();
        CreateMap<AddressModel, AddressDTO>();

        CreateMap<ClientModel, UserDTO>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "client"))
            .ForMember(dest => dest.Newsletter, opt => opt.MapFrom(src => src.HasNewsletterOn))
            .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.ClientID));
        CreateMap<EmployeeModel, UserDTO>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "employee"))
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.Newsletter, opt => opt.Ignore())
            .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.EmployeeID));
        CreateMap<DeliveryManModel, UserDTO>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "deliveryman"))
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.Newsletter, opt => opt.Ignore())
            .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.DeliveryManID));

        CreateMap<NewUserDTO, ClientModel>()
            .ForMember(dest => dest.HasNewsletterOn, opt => opt.MapFrom(src => src.Newsletter));
        CreateMap<NewUserDTO, EmployeeModel>();
        CreateMap<NewUserDTO, DeliveryManModel>();

        CreateMap<NewProductDTO, ProductModel>()
            .ForMember(dest => dest.Archived, opt => opt.MapFrom(src => false));

        CreateMap<ProductModel, ProductDTO>(); //Modification may be needed later to handle getting products from database

        CreateMap<UpdateProductDTO, ProductModel>();

        CreateMap<OrderedItemDTO, OrderDetailsModel>();
        CreateMap<OrderDetailsModel, OrderedItemDTO>();

        CreateMap<OrderModel, OrderDTO>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderDetails));

        CreateMap<NewOrderDTO, OrderModel>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "received"))
            .ForMember(dest => dest.Complaints, opt => opt.Ignore())
            .ForMember(dest => dest.DeliveryMan, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());
    }
}

