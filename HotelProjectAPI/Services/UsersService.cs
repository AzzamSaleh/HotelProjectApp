using HotelProjectAPI.Constants;
using HotelProjectAPI.Contracts;
using HotelProjectAPI.DTOs.Auth;
using HotelProjectAPI.Models;
using HotelProjectAPI.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelProjectAPI.Services;

public class UsersService(UserManager<ApplicationUser> userManager,
    IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : IUsersService
{
    public async Task<Result<RegisteredUserDto>> RegisterAsync(RegisterUserDto registerUserDto)
    {
        // create a new ApplicationUser instance and set its properties based on the registerUserDto
        var user = new ApplicationUser
        {
            Email = registerUserDto.Email,
            FirstName = registerUserDto.FirstName,
            LastName = registerUserDto.LastName,
            UserName = registerUserDto.Email
        };
        // use the UserManager to create the user in the database with the provided password
        var result = await userManager.CreateAsync(user, registerUserDto.Password);

        //check if the user creation was successful
        if (!result.Succeeded)
        {
            // If the user creation failed, return a bad request result with the error messages
            var errors = result.Errors.Select(e => new Error(ErrorCodes.BadRequest, e.Description)).ToArray();
            return Result<RegisteredUserDto>.BadRequest(errors);
        }

        // Assign the role to the user
        await userManager.AddToRoleAsync(user, registerUserDto.Role);

        // Create a RegisteredUserDto to return the registered user's information
        var registeredUser = new RegisteredUserDto
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Id = user.Id,
            Role = registerUserDto.Role,
        };

        // Optional: Send confirmation Emai
        return Result<RegisteredUserDto>.Success(registeredUser);

    }
    public async Task<Result<string>> LoginAsync(LoginUserDto dto)
    {

        // Find the user by email
        var user = await userManager.FindByEmailAsync(dto.Email);
        // Check if the user exists and the password is correct
        if (user is null)
        {
            return Result<string>.Failure(new Error(ErrorCodes.BadRequest, "Invalid credentials."));
        }
        // Check if the password is correct
        var valid = await userManager.CheckPasswordAsync(user, dto.Password);
        // If the password is incorrect, return an error
        if (!valid)
        {
            return Result<string>.Failure(new Error(ErrorCodes.BadRequest, "Invalid credentials."));
        }

        // Issue a token
        var token = await GenerateToken(user);

        return Result<string>.Success(token);
    }

    private async Task<string> GenerateToken(ApplicationUser user)
    {
        // Set basic user claims
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, user.Id),
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Name, user.FullName)
        };

        // Set user role claims
        var roles = await userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

        claims = claims.Union(roleClaims).ToList();

        // Set JWT Key credentials
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"] ?? string.Empty));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Create an encoded token
        var token = new JwtSecurityToken(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["JwtSettings:DurationInMinutes"])),
            signingCredentials: credentials
            );

        // Return token value
        return new JwtSecurityTokenHandler().WriteToken(token);
    }



    public string UserId => httpContextAccessor?
   .HttpContext?
   .User?
   .FindFirst(JwtRegisteredClaimNames.Sub)?.Value
?? httpContextAccessor?
   .HttpContext?
   .User?
   .FindFirst(ClaimTypes.NameIdentifier)?.Value
?? string.Empty;
}
