using HotelProjectAPI.Contracts;
using HotelProjectAPI.DTOs.Booking;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelProjectAPI.Controllers
{

    // [Route("api/[controller]")]
    [Route("api/hotels/{hotelId:int}/bookings")]
    [ApiController]
    public class HotelBookingsController(IBookingService bookingService) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBookingDto>>> GetBookings([FromRoute] int hotelId)
        {
            var result = await bookingService.GetBookingsForHotelAsync(hotelId);
            return ToActionResult(result);
        }



        [HttpPost]
        public async Task<ActionResult<GetBookingDto>> CreateBooking([FromRoute] int hotelId, [FromBody] CreateBookingDto createBookingDto)
        {
            var result = await bookingService.CreateBookingAsync(createBookingDto);
            return ToActionResult(result);
        }



        [HttpPut("{bookingId:int}")]
        public async Task<ActionResult<GetBookingDto>> UpdateBooking( [FromRoute] int hotelId,
       [FromRoute] int bookingId,
       [FromBody] UpdateBookingDto updateBookingDto)
        {
            var result = await bookingService.UpdateBookingAsync(hotelId, bookingId, updateBookingDto);
            return ToActionResult(result);
        }



    }
}
