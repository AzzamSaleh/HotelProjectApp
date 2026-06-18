using System.ComponentModel.DataAnnotations;

namespace HotelProjectAPI.DTOs.Country;

public class CreateCountryDto
{
    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Code { get; set; }
}
