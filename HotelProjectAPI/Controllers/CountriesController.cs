using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelProjectAPI.Data;
using HotelProjectAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    //object that represent the connection to the database
    private readonly HotelProjectDbContext _context;//DI  
    public CountriesController(HotelProjectDbContext context)//DI
    {
        _context = context;
    }

    // GET: api/Country
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> GetCountry()
    {
        //SELECT * FROM Countries
        return await _context.Countries.ToListAsync();
    }

    // GET: api/Country/5
    [HttpGet("{countryid}")]
    public async Task<ActionResult<Country>> GetCountry(int countryid)
    {
        var country = await _context.Countries.FindAsync(countryid);

        if (country == null)
        {
            return NotFound();//404
        }

        return country;
    }

    // PUT: api/Country/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{countryid}")]
    public async Task<IActionResult> PutCountry(int? countryid, Country country)
    {
        if (countryid != country.CountryId)
        {
            return BadRequest();//400
        }

        _context.Entry(country).State = EntityState.Modified;//

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CountryExists(countryid))//if not exist
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();//204
    }

    // POST: api/Country
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Country>> PostCountry(Country country)
    {
        _context.Countries.Add(country);
        await _context.SaveChangesAsync();//201

        return CreatedAtAction("GetCountry", new { countryid = country.CountryId }, country);//returing the rout to get that new record
    }

    // DELETE: api/Country/5
    [HttpDelete("{countryid}")]
    public async Task<IActionResult> DeleteCountry(int? countryid)
    {
        var country = await _context.Countries.FindAsync(countryid);
        if (country == null)
        {
            return NotFound();
        }

        _context.Countries.Remove(country);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CountryExists(int? countryid)
    {
        return _context.Countries.Any(e => e.CountryId == countryid);//linq to check if the record exist in the database
    }
}
