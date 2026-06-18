using System.ComponentModel.DataAnnotations;

namespace HotelProjectAPI.DTOs.Hotel;

public class UpdateHotelDto : CreateHotelDto
{
    //Inherit all properties from CreateHotelDto
    [Required]
    public int Id { get; set; }
}