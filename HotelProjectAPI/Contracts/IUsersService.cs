using HotelProject.Api.Common.Results;
using HotelProjectAPI.DTOs.Auth;

namespace HotelProjectAPI.Contracts;

public interface IUsersService
{
    string UserId { get; }

    Task<Result<string>> LoginAsync(LoginUserDto dto);
    Task<Result<RegisteredUserDto>> RegisterAsync(RegisterUserDto registerUserDto);
}
