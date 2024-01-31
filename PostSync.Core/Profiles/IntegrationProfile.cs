using AutoMapper;
using PostSync.Core.DTOs.Responses;
using PostSync.Core.Models;

namespace PostSync.Infrastructure.Profiles;

public class IntegrationProfile : Profile
{
    public IntegrationProfile()
    {
        CreateMap<IntegrationTokens,IntegrationSessionModel>()
            .ForMember(dest=> dest.AtExpire, map => map.MapFrom(token => DateTime.Now.AddSeconds(token.ATokenExpire)))
            .ForMember(dest=> dest.RtExpire, map => map.MapFrom(token => DateTime.Now.AddSeconds(token.RTokenExpire)))
            .ReverseMap();
    }
}