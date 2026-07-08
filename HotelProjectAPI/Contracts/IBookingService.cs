using HotelProjectAPI.DTOs.Booking;
using HotelProjectAPI.Results;

namespace HotelProjectAPI.Contracts
{
    public interface IBookingService
    {
        Task<Result> AdminCancelBookingAsync(int hotelId, int bookingId);
        Task<Result> AdminConfirmBookingAsync(int hotelId, int bookingId);
        Task<Result> CancelBookingAsync(int hotelId, int bookingId);
        Task<Result<GetBookingDto>> CreateBookingAsync(CreateBookingDto dto);
        Task<Result<IEnumerable<GetBookingDto>>> GetBookingsForHotelAsync(int hotelId);
        Task<Result<IEnumerable<GetBookingDto>>> GetUserBookingsForHotelAsync(int hotelId);
        Task<Result<GetBookingDto>> UpdateBookingAsync(int hotelId, int bookingId, UpdateBookingDto dto);
    }
}