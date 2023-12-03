using AutoMapper;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.Core.Domain;

namespace Explorer.Payments.Core.Mappers;

public class PaymentsProfile : Profile
{
    public PaymentsProfile()
    {
        CreateMap<TourPurchaseTokenDto, TourPurchaseToken>().ReverseMap();
        CreateMap<ShoppingCartDto, ShoppingCart>().ReverseMap()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.GetTotal()));
        CreateMap<ItemDto, OrderItem>().ReverseMap();
        CreateMap<ItemDto, Item>().ReverseMap();
        CreateMap<CouponDto, Coupon>().ReverseMap();
        CreateMap<CreateCouponDto, Coupon>().ReverseMap();
        CreateMap<ShoppingCartDto, ShoppingCart>().ReverseMap();
        CreateMap<OrderItemDto, OrderItem>().ReverseMap();
        CreateMap<SaleDto, Sale>().ReverseMap();
    }
}