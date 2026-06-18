using HotelProjectAPI.Contracts;
using HotelProjectAPI.Data;
using HotelProjectAPI.DTOs.Country;
using HotelProjectAPI.DTOs.Hotel;
using HotelProjectAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelProjectAPI.Services;

public class CountriesService(HotelProjectDbContext context) : ICountriesService
{

    public async Task<IEnumerable<GetCountriesDto>> GetCountriesAsync()
    {
        var contries = await context.Countries
            .Select(c => new GetCountriesDto(c.CountryId, c.Name, c.Code))
            .ToListAsync();
        return contries;
    }

    public async Task<GetCountryDto?> GetCountryAsync(int countryid)
    {
        var country = await context.Countries
            .Where(c => c.CountryId == countryid)
            .Select(c => new GetCountryDto(c.CountryId, c.Name, c.Code,
            c.Hotels.Select(h => new GetHotelSlimDto(
                h.Id,
                h.Name,
                h.Address,
                h.Rating
                )).ToList()
            ))
            .FirstOrDefaultAsync();
        return country ?? null;
    }

    public async Task<GetCountryDto> CreateCountryAsync(CreateCountryDto createDto)
    {

        var country = new Country
        {
            Name = createDto.Name,
            Code = createDto.Code
        };
        context.Countries.Add(country);
        await context.SaveChangesAsync();//201
        return new GetCountryDto(country.CountryId, country.Name, country.Code, []);
    }

    public async Task UpdateCountryAsync(int countryid, UpdateCountryDto updateDto)
    {
        var country = await context.Countries.FindAsync(countryid) ?? throw new KeyNotFoundException 
            ("Country not found");
      
        country.Name = updateDto.Name;
        country.Code = updateDto.Code;
        context.Countries.Update(country);
        await context.SaveChangesAsync();
        
    }
    public async Task DeleteCountryAsync(int countryid)
    {

        var country = await context.Countries.FindAsync(countryid) ?? throw new KeyNotFoundException 
            ("Country not found");
        context.Countries.Remove(country);
         context.SaveChangesAsync();
    }


    public async Task<bool> CountryExistsAsync(int? countryid)
    {
        return await context.Countries.AnyAsync(e => e.CountryId == countryid);//linq to check if the record exist in the database
    }
}