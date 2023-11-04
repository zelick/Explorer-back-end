using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Dtos.Shopping;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Shopping;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
	public StakeholderProfile()
    {
        CreateMap<ClubRequestDto, ClubRequest>().ReverseMap();
        CreateMap<ClubDto, Club>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<ClubInvitationDto, ClubInvitation>().ReverseMap();
        CreateMap<ApplicationGradeDto, ApplicationGrade>().ReverseMap();
        CreateMap<PersonDto, Person>().ReverseMap();
        CreateMap<ClubDto, Club>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<CustomerDto, Customer>().ReverseMap();
        CreateMap<TourPurchaseTokenDto, TourPurchaseToken>().ReverseMap();
        CreateMap<ShoppingCartDto, ShoppingCart>().ReverseMap();
        CreateMap<OrderItemDto, OrderItem>().ReverseMap();

    }
}