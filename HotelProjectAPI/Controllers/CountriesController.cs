using HotelProjectAPI.DTOs.Country;
using HotelProjectAPI.Models;
using HotelProjectAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CountriesController(CountriesService countriesService) : ControllerBase
{

    // GET: api/Country
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCountriesDto>>> GetCountry()
    {
        var contries = await countriesService.GetCountriesAsync();
        return Ok(contries);
    }

        // GET: api/Country/5
        [HttpGet("{countryid}")]
    public async Task<ActionResult<GetCountryDto>> GetCountry(int countryid)
    {

        var country = await countriesService.GetCountryAsync(countryid);

        if (country == null)
        {
            return NotFound();//404
        }

        return country;
    }

    // PUT: api/Country/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{countryid}")]
    public async Task<IActionResult> PutCountry(int countryid, UpdateCountryDto countryDto)
    {
        if (countryid != countryDto.Id)
        {
            return BadRequest();//400
        }

        await countriesService.UpdateCountryAsync(countryid, countryDto);
        return NoContent();
    }

    // POST: api/Country
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Country>> PostCountry(CreateCountryDto countryDto)
    {
        var resultDto = await countriesService.CreateCountryAsync(countryDto);


        return CreatedAtAction(nameof(GetCountry), new { countryid = resultDto.Id }, resultDto);
    }

    // DELETE: api/Country/5
    [HttpDelete("{countryid}")]
    public async Task<IActionResult> DeleteCountry(int countryid)
    {
        await countriesService.DeleteCountryAsync(countryid);
        return NoContent();
    }

    
}
