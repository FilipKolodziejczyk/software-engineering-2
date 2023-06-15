using AutoMapper;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Profiles;

public class AutoMapperProfile : Profile {
    public AutoMapperProfile() {
        CreateMap<AddressDto, AddressModel>();
        CreateMap<AddressModel, AddressDto>();

        CreateMap<ClientModel, UserDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Roles.Client))
            .ForMember(dest => dest.Newsletter, opt => opt.MapFrom(src => src.HasNewsletterOn))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.ClientId));
        CreateMap<EmployeeModel, UserDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Roles.Employee))
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.Newsletter, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.EmployeeId));
        CreateMap<DeliveryManModel, UserDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Roles.DeliveryMan))
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.Newsletter, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.DeliveryManId));

        CreateMap<NewUserDto, ClientModel>()
            .ForMember(dest => dest.HasNewsletterOn, opt => opt.MapFrom(src => src.Newsletter));
        CreateMap<NewUserDto, EmployeeModel>();
        CreateMap<NewUserDto, DeliveryManModel>();

        CreateMap<NewProductDto, ProductModel>()
            .ForMember(dest => dest.Archived, opt => opt.MapFrom(src => false));

        CreateMap<ProductModel, ProductDto>()
            .IncludeAllDerived()
            .ForMember(dest => dest.ImageUris, opt => opt.MapFrom(src => src.Images))
            .ForMember(dest => dest.ImageIds, opt => opt.MapFrom(src => src.Images));
        CreateMap<ImageModel, Uri>().ConstructUsing(image => image.ImageUri);
        CreateMap<ImageModel, int>().ConstructUsing(image => image.ImageId);
            
        CreateMap<ImageModel, ImageDto>();

        CreateMap<UpdateProductDto, ProductModel>();

        CreateMap<OrderedItemDto, OrderDetailsModel>();
        CreateMap<OrderDetailsModel, OrderedItemDto>();

        CreateMap<OrderModel, OrderDto>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderDetails))
            .ForMember(dest => dest.DeliveryMan, opt => opt.MapFrom(src => src.DeliveryMan));

        CreateMap<NewOrderDto, OrderModel>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => OrderStatus.Received))
            .ForMember(dest => dest.Complaints, opt => opt.Ignore())
            .ForMember(dest => dest.DeliveryMan, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());

        CreateMap<BasketItemModel, BasketItemDto>();

        CreateMap<BasketItemDto, BasketItemModel>()
            .ForMember(dest => dest.ClientId, opt => opt.Ignore());
    }
}

