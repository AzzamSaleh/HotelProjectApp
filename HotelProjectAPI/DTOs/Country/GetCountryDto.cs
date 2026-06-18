using HotelProjectAPI.DTOs.Hotel;

namespace HotelProjectAPI.DTOs.Country;

public record GetCountryDto(
    int Id,
    string Name,
    string Code,
    List<GetHotelSlimDto> Hotels
    );

public record GetCountriesDto(
    int Id,
    string Name,
    string Code
    );