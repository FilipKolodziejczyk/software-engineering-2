using AutoMapper;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Profiles;

public class AutoMapperProfile : Profile {
    public AutoMapperProfile() {
        CreateMap<AddressDTO, AddressModel>();
        CreateMap<AddressModel, AddressDTO>();

        CreateMap<ClientModel, UserDTO>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Roles.Client))
            .ForMember(dest => dest.Newsletter, opt => opt.MapFrom(src => src.HasNewsletterOn))
            .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.ClientID));
        CreateMap<EmployeeModel, UserDTO>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Roles.Employee))
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.Newsletter, opt => opt.Ignore())
            .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.EmployeeID));
        CreateMap<DeliveryManModel, UserDTO>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Roles.DeliveryMan))
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.Newsletter, opt => opt.Ignore())
            .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.DeliveryManID));

        CreateMap<NewUserDTO, ClientModel>()
            .ForMember(dest => dest.HasNewsletterOn, opt => opt.MapFrom(src => src.Newsletter));
        CreateMap<NewUserDTO, EmployeeModel>();
        CreateMap<NewUserDTO, DeliveryManModel>();

        CreateMap<NewProductDTO, ProductModel>()
            .ForMember(dest => dest.Archived, opt => opt.MapFrom(src => false));

        CreateMap<ProductModel, ProductDTO>();

        CreateMap<UpdateProductDTO, ProductModel>();

        CreateMap<OrderedItemDTO, OrderDetailsModel>();
        CreateMap<OrderDetailsModel, OrderedItemDTO>();

        CreateMap<OrderModel, OrderDTO>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderDetails))
            .ForMember(dest => dest.DeliveryMan, opt => opt.MapFrom(src => src.DeliveryMan));

        CreateMap<NewOrderDTO, OrderModel>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => OrderStatus.Received))
            .ForMember(dest => dest.Complaints, opt => opt.Ignore())
            .ForMember(dest => dest.DeliveryMan, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());

        CreateMap<BasketItemModel, BasketItemDTO>();

        CreateMap<BasketItemDTO, BasketItemModel>()
            .ForMember(dest => dest.ClientID, opt => opt.Ignore());
    }
}

