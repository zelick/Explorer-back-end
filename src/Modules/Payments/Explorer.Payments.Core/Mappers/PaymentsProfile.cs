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
        CreateMap<OrderItemDto, OrderItem>().ReverseMap();
        CreateMap<CouponDto, Coupon>().ReverseMap();
        CreateMap<CreateCouponDto, Coupon>().ReverseMap();
    }
}