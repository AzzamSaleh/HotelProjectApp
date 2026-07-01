using HotelProjectAPI.DTOs.Hotel;

namespace HotelProjectAPI.DTOs.Country;


public class GetCountryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public List<GetHotelSlimDto> Hotels { get; set; } = new();
}

public class GetCountriesDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}