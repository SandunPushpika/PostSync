using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using DevOne.Security.Cryptography.BCrypt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PostSync.Core.DTOs.Requests;
using PostSync.Core.DTOs.Responses;
using PostSync.Core.Helpers.Configs;
using PostSync.Core.Interfaces;
using PostSync.Core.Services;

namespace PostSync.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly ResponseService _responseService;
    private readonly AppConfig _appConfig;
    
    public AuthService(IUserService userService, ResponseService responseService, IOptions<AppConfig> options)
    {
        _userService = userService;
        _responseService = responseService;
        _appConfig = options.Value;
    }

    public async Task<TokenResponse> Login(UserLoginRequest request)
    {
        var user = await _userService.GetByUsername(request.Username);

        if (user == null)
        {
            _responseService.StatusCode = HttpStatusCode.NotFound;
            _responseService.Message = "Username doesn't exist!";
            return null;
        }

        var result = BCryptHelper.CheckPassword(request.Password, user.Password);

        if (!result)
        {
            _responseService.StatusCode = HttpStatusCode.Unauthorized;
            _responseService.Message = "Incorrect credentials!";
            return null;
        }

        var accessToken = CreateAccessToken(user.Id, user.UserName, _appConfig.Jwt.Issuer, _appConfig.Jwt.Audience,
            _appConfig.Jwt.Key);
        var refreshToken = CreateRefreshToken(user.UserName, _appConfig.Jwt.Issuer, _appConfig.Jwt.Audience,
            _appConfig.Jwt.Key);
        
        return new TokenResponse()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
    
    private string CreateAccessToken(int userId, string username ,string issuer, string audience, string key)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var claims = new List<Claim>()
        {
            new Claim("userId",userId.ToString()),
            new Claim("username",username)
        };

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            SigningCredentials = credentials,
            Subject = new ClaimsIdentity(claims),
            Audience = audience,
            Issuer = issuer,
            Expires = DateTime.Now.AddHours(1)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string CreateRefreshToken(string username, string issuer, string audience, string key)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var claims = new List<Claim>()
        {
            new Claim("username", username)
        };
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            SigningCredentials = credentials,
            Subject = new ClaimsIdentity(claims),
            Audience = audience,
            Issuer = issuer,
            Expires = DateTime.Now.AddDays(7)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<TokenResponse> RefreshToken(string token)
    {
        var jwtToken = await GetValidatedToken(token);

        if (jwtToken == null)
            return null;
        
        var usernameClaim = jwtToken.ClaimsIdentity.Claims.ToList().FirstOrDefault(claim => claim.Type == "username");
        if (usernameClaim == null)
        {
            _responseService.StatusCode = HttpStatusCode.Unauthorized;
            _responseService.Message = "Invalid Token!";
            return null;
        }

        var user = await _userService.GetByUsername(usernameClaim.Value);
        if (user == null)
        {
            _responseService.StatusCode = HttpStatusCode.Unauthorized;
            _responseService.Message = "Invalid user!";
            return null;
        }

        var accessToken = CreateAccessToken(user.Id, user.UserName, _appConfig.Jwt.Issuer, _appConfig.Jwt.Audience,
            _appConfig.Jwt.Key);
        var refreshToken = CreateRefreshToken(user.UserName, _appConfig.Jwt.Issuer, _appConfig.Jwt.Audience,
            _appConfig.Jwt.Key);
        
        return new TokenResponse()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<int?> GetUserId(string token)
    {
        var jwtToken = await GetValidatedToken(token);
        if (jwtToken == null)
            return null;

        var userIdClaim = jwtToken.ClaimsIdentity.Claims.FirstOrDefault(claim => claim.Type == "userId");
        if (userIdClaim == null)
            return null;

        return int.Parse(userIdClaim.Value);
    }

    private async Task<TokenValidationResult> GetValidatedToken(string token)
    {
        if (token == null)
            return null;
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var jwtToken = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters()
        {
            RequireExpirationTime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfig.Jwt.Key)),
            ValidIssuer = _appConfig.Jwt.Issuer,
            ValidAudience = _appConfig.Jwt.Audience,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        });

        if (!jwtToken.IsValid)
        {
            _responseService.StatusCode = HttpStatusCode.Unauthorized;
            _responseService.Message = "Invalid Token!";
            return null;
        }

        return jwtToken;
    }
}