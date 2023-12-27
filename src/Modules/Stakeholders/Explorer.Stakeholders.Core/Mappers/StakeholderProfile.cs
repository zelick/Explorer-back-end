using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;

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
        CreateMap<MessageDto,  Message>().ReverseMap();
        CreateMap<SocialProfileDto, SocialProfile>().ReverseMap();
        CreateMap<NotificationDto, Notification>().ReverseMap();
        CreateMap<ObjectRequestDto, ObjectRequest>().ReverseMap();
        CreateMap<CheckpointRequestDto, CheckpointRequest>().ReverseMap();
        CreateMap<User, Tourist>().ReverseMap();
        CreateMap<Tourist, TouristDto>();
        CreateMap<SecureToken, SecureTokenDto>().ReverseMap();
    }
}