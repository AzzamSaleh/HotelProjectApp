using System.ComponentModel.DataAnnotations;

namespace HotelProject.Api.Application.DTOs.Auth;

public class RegisterUserDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(8)]
    public string Password { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    //accept a role parameter to specify the role of the user being registered.
    //If no role is provided, it defaults to "User".
    public string Role { get; set; } = "User";
}
