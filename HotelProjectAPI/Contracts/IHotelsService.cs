using HotelProjectAPI.DTOs.Hotel;
//GetAll, GetById, Create, Update, Delete

public interface IHotelsService
{
    Task<bool> HotelExistsAsync(int? Hotelid);
    Task<GetHotelDto> CreateHotelAsync(CreateHotelDto createDto);
    Task DeleteHotelAsync(int Hotelid);
    Task<IEnumerable<GetHotelsDto>> GetHotelsAsync();
    Task<GetHotelDto?> GetHotelAsync(int Hotelid);
    Task UpdateHotelAsync(int Hotelid, UpdateHotelDto updateDto);
}