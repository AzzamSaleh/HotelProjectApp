using HotelProjectAPI.DTOs.Country;

namespace HotelProjectAPI.Contracts
{
    public interface ICountriesService
    {
        Task<bool> CountryExistsAsync(int? countryid);
        Task<GetCountryDto> CreateCountryAsync(CreateCountryDto createDto);
        Task DeleteCountryAsync(int countryid);
        Task<IEnumerable<GetCountriesDto>> GetCountriesAsync();
        Task<GetCountryDto?> GetCountryAsync(int countryid);
        Task UpdateCountryAsync(int countryid, UpdateCountryDto updateDto);
    }
}
