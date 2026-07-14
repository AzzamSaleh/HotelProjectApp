using HotelProject.Api.Common.Results;
using HotelProjectAPI.DTOs.Hotel;

namespace HotelProjectAPI.Services
{
    public interface IHotelsService
    {
        Task<Result<GetHotelDto>> CreateHotelAsync(CreateHotelDto hotelDto);
        Task<Result> DeleteHotelAsync(int id);
        Task<Result<GetHotelDto>> GetHotelAsync(int id);
        Task<Result<IEnumerable<GetHotelDto>>> GetHotelsAsync();
        Task<bool> HotelExistsAsync(int id);
        Task<bool> HotelExistsAsync(string name, int countryId);
        Task<Result> UpdateHotelAsync(int id, UpdateHotelDto updateDto);
    }
}