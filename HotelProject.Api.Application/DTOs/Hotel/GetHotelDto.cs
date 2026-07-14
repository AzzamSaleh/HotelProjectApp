namespace HotelProject.Api.Application.DTOs.Hotel;

//public record GetHotelDto(
//    int Id,
//    string Name,
//    string Address,
//    double Rating,
//    string Country //wante include country details in the response
//)


public record GetHotelDto(
    int Id,
    string Name,
    string Address,
    double Rating,
    int CountryId,
    string CountryName);