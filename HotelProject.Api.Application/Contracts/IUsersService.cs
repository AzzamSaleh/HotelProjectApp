using HotelProject.Api.Application.DTOs.Auth;
using HotelProject.Api.Common.Results;

namespace HotelProject.Api.Application.Contracts;

public interface IUsersService
{
    string UserId { get; }

    Task<Result<string>> LoginAsync(LoginUserDto dto);
    Task<Result<RegisteredUserDto>> RegisterAsync(RegisterUserDto registerUserDto);
}
