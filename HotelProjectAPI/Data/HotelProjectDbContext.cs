using HotelProjectAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace HotelProjectAPI.Data
{
    public class HotelProjectDbContext : DbContext
    {
        public HotelProjectDbContext(DbContextOptions<HotelProjectDbContext> options) : base(options)

        {
        }

     

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
    }
}
