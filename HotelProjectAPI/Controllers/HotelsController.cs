using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelProjectAPI.Models;
using HotelProjectAPI.Data;
using HotelProjectAPI.DTOs.Hotel;
using HotelProjectAPI.Services;

[Route("api/[controller]")]
[ApiController]
public class HotelsController(HotelsService hotelsService) : ControllerBase
{

    // GET: api/Hotel
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetHotelsDto>>> GetHotel()
    {

        var hotels = await hotelsService.GetHotelsAsync();
        return Ok(hotels);
      
  
    }

    // GET: api/Hotel/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetHotelDto>> GetHotel(int id)
    {
        var hotel = await hotelsService.GetHotelAsync(id);

        if (hotel == null)
        {
            return NotFound();
        }

        return hotel;
    }

    // PUT: api/Hotel/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHotel(int? id, UpdateHotelDto hotelDto)
    {
        if (id != hotelDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await hotelsService.UpdateHotelAsync(id.Value, hotelDto);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Hotel
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<GetHotelDto>> PostHotel(CreateHotelDto hotelDto)
    {
        var resultDto = await hotelsService.CreateHotelAsync(hotelDto);

        return CreatedAtAction(nameof(GetHotel), new { id = resultDto.Id }, resultDto);
    }

    // DELETE: api/Hotel/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotel(int? id)
    {
        try
        {
            await hotelsService.DeleteHotelAsync(id.Value);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
