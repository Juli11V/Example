using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebAPI.Application.DTOs;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Services;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterRequest, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.Ignore());
        CreateMap<TransactionExternal, Transaction>()
    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
    .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
    .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp))
    .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType));

    }
}