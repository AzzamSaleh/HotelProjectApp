using HotelProjectAPI.DTOs.Booking;
using HotelProjectAPI.Results;

namespace HotelProjectAPI.Contracts
{
    public interface IBookingService
    {
        Task<Result<GetBookingDto>> CreateBookingAsync(CreateBookingDto dto);
        Task<Result<IEnumerable<GetBookingDto>>> GetBookingsForHotelAsync(int hotelId);
    }
}