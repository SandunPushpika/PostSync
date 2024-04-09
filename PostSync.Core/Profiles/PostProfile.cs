using AutoMapper;
using PostSync.Core.DTOs.Requests;
using PostSync.Core.Models;

namespace PostSync.Infrastructure.Profiles;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<PostCreateRequest, PostModel>().ReverseMap();
    }
}