using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HR.LeaveManagement.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
    }
    
    public async Task<AuthResponse> LoginAsync(AuthRequest request)
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

        var response = new AuthResponse
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
            Email = user.Email,
            UserName = user.UserName,
        };
        
        return response;
    }
    
    public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
    {
        var user = new User
        {
            AspNetUser = new IdentityUser()
            {
                Email = request.Email,
                UserName = request.Email,
            },
            FirstName = request.FirstName,
            LastName = request.LastName,
        };
        
        var result = await _userManager.CreateAsync(user.AspNetUser, request.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user.AspNetUser, "Employee");
            return new RegistrationResponse() { UserId = user.AspNetUser.Id };
        }

        var str = new StringBuilder();
        foreach (var error in result.Errors)
        {
            str.AppendFormat("{0}\n",error.Description);
        }
        throw new BadRequestException($"{str}");
    }

    private async Task<JwtSecurityToken> GenerateToken(IdentityUser user)   
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);
        
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signInCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signInCredentials
        );
        
        return jwtSecurityToken;
    }

   
}