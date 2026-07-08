using HotelProjectAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelProjectAPI.Data.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{

    //to convert enum to string in database
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.Property(q => q.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        //Add indexes for performance optimization
        //Speed up read operations on queried columns
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.HotelId);
        builder.HasIndex(x => new { x.CheckIn, x.CheckOut });
    }
}
