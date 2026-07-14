namespace HotelProject.Api.Domain.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public required string? Name { get; set; }
        public required string? Code { get; set; }
        public IList<Hotel> Hotels { get; set; } = [];
    }
}

