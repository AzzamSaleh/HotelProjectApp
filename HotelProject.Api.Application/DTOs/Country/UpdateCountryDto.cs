using System.ComponentModel.DataAnnotations;

namespace HotelProject.Api.Application.DTOs.Country;

public class UpdateCountryDto : CreateCountryDto
{
    [Required]
    public int Id { get; set; }
}