using AutoMapper;
using PostSync.Core.Domain.Models;
using PostSync.Core.Domain.Requests;

namespace PostSync.Infrastructure.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserModel, UserCreateRequest>().ReverseMap();
    }
}