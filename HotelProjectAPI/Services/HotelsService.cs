using AutoMapper;
using HotelProjectAPI.Data;
using HotelProjectAPI.DTOs.Hotel;
using HotelProjectAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelProjectAPI.Services
{
    public class HotelsService(HotelProjectDbContext context, IMapper mapper ) : IHotelsService
    {

        public async Task<IEnumerable<GetHotelsDto>> GetHotelsAsync()
        {
            var hotels = await context.Hotels
                .Select(h => new GetHotelsDto(h.Id, h.Name, h.Address, h.Rating, h.CountryId))
                .ToListAsync();
            return hotels;
        }

        public async Task<GetHotelDto?> GetHotelAsync(int hotelid)
        {
            var hotel = await context.Hotels
                .Where(h => h.Id == hotelid)
                .Select(h => new GetHotelDto(h.Id, h.Name, h.Address, h.Rating, h.Country.Name))
                .FirstOrDefaultAsync();
            return hotel;
        }

        public async Task<GetHotelDto> CreateHotelAsync(CreateHotelDto createDto)
        {

            var hotel = new Hotel
            {
                Name = createDto.Name,
                Address = createDto.Address,
                Rating = createDto.Rating,
                CountryId = createDto.CountryId
            };
            context.Hotels.Add(hotel);
            await context.SaveChangesAsync();//201
            return new GetHotelDto(hotel.Id, hotel.Name, hotel.Address, hotel.Rating, string.Empty);
        }

        public async Task UpdateHotelAsync(int hotelid, UpdateHotelDto updateDto)
        {
            var hotel = await context.Hotels.FindAsync(hotelid) ?? throw new KeyNotFoundException
                ("Hotel not found");

            hotel.Name = updateDto.Name;
            hotel.Address = updateDto.Address;
            hotel.Rating = updateDto.Rating;
            hotel.CountryId = updateDto.CountryId;
            context.Hotels.Update(hotel);
            await context.SaveChangesAsync();

        }
        public async Task DeleteHotelAsync(int hotelid)
        {

            var hotel = await context.Hotels.FindAsync(hotelid) ?? throw new KeyNotFoundException
                ("Hotel not found");
            context.Hotels.Remove(hotel);
            await context.SaveChangesAsync();
        }


        public async Task<bool> HotelExistsAsync(int? hotelid)
        {
            return await context.Hotels.AnyAsync(e => e.Id == hotelid);//linq to check if the record exist in the database
        }
    }
}
