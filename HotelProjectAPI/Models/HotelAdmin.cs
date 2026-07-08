namespace HotelProjectAPI.Models;

//many to many relationship between Hotel and ApplicationUser through HotelAdmin
public class HotelAdmin
{
    public int Id { get; set; }

    public Hotel? Hotel { get; set; }
    public int HotelId { get; set; }

    public ApplicationUser? User { get; set; }
    public required string UserId { get; set; }
}