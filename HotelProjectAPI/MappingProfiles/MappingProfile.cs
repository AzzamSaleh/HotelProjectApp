using AutoMapper;
using HotelProjectAPI.DTOs.Country;
using HotelProjectAPI.DTOs.Hotel;
using HotelProjectAPI.Models;

namespace HotelProjectAPI.MappingProfiles;

public class HotelMappingProfile : Profile
{
    public HotelMappingProfile()
    {
        CreateMap<Hotel, GetHotelDto>()//have issue with prop that have the same name, one have a sting type and other have a country object type
            .ForMember(d => d.Country, cfg => cfg.MapFrom(s => s.Country!.Name));
        CreateMap<CreateHotelDto, Hotel>();
    }
}

public class CountryMappingProfile : Profile
{
    public CountryMappingProfile()
    {
        CreateMap<Country, GetCountryDto>();

        CreateMap<Country, GetCountriesDto>();

        CreateMap<CreateCountryDto, Country>();

    }
}
