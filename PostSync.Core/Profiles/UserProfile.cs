using AutoMapper;
using PostSync.Core.DTOs.Requests;
using PostSync.Core.Models;

namespace PostSync.Infrastructure.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserModel, UserCreateRequest>().ReverseMap();
    }
}