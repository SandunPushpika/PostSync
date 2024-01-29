using System.Net;
using AutoMapper;
using DevOne.Security.Cryptography.BCrypt;
using PostSync.Core.DTOs.Requests;
using PostSync.Core.Interfaces;
using PostSync.Core.Models;
using PostSync.Core.Services;
using PostSync.Infrastructure.Queries;

namespace PostSync.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IDbContext _context;
    private readonly IMapper _mapper;
    private readonly ResponseService _responseService;
    
    public UserService(IDbContext context, IMapper mapper, ResponseService responseService)
    {
        _context = context;
        _mapper = mapper;
        _responseService = responseService;
    }

    public async Task<UserModel> GetByUsername(string username)
    {
        var model = await _context.GetAsync<UserModel>(UserQueries.GET_BY_USERNAME, new { Username = username });
        return model;
    }
    
    public async Task<bool> CreateUser(UserCreateRequest createRequest)
    {

        var user = await GetByUsername(createRequest.UserName);

        if (user != null)
        {
            _responseService.StatusCode = HttpStatusCode.Conflict;
            _responseService.Message = "Username already exists";
            return false;   
        }
        
        var userModel = _mapper.Map<UserModel>(createRequest);
        userModel.Status = UserStatus.PENDING_VERIFICATION;
        userModel.Password = BCryptHelper.HashPassword(createRequest.Password, GetSalt());

        var result = await _context.ExecuteAsync(UserQueries.INSERT_USER, userModel);
        
        return result != 0;
    }

    private string GetSalt()
    {
        return BCryptHelper.GenerateSalt();
    }
    
}