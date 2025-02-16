using System.IdentityModel.Tokens.Jwt;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace HR.LeaveMangement.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
    }
    
    public async Task<AuthResponse> IsUserLoggedIn(AuthRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new NotFoundException("User not found", request.Email);
        }
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (result.Succeeded is false)
        {
            throw new BadRequestException("Invalid login or password");
        }

        JwtSecurityToken securityToken = await GenerateToken(user);
    }

    private async Task<JwtSecurityToken> GenerateToken(User user)   
    {
        
    }

    public Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        throw new NotImplementedException();
    }
}