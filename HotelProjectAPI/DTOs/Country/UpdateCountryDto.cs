using System.ComponentModel.DataAnnotations;

namespace HotelProjectAPI.DTOs.Country;

public class UpdateCountryDto : CreateCountryDto
{
    [Required]
    public int Id { get; set; }
}