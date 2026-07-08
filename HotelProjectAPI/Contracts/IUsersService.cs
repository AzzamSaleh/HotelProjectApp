using HotelProjectAPI.DTOs.Auth;
using HotelProjectAPI.Results;

namespace HotelProjectAPI.Contracts;

public interface IUsersService
{
    string UserId { get; }

    Task<Result<string>> LoginAsync(LoginUserDto dto);
    Task<Result<RegisteredUserDto>> RegisterAsync(RegisterUserDto registerUserDto);
}
