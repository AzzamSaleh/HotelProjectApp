using HotelProjectAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HotelProjectAPI.Data;

public class HotelProjectDbContext : IdentityDbContext<ApplicationUser>
    //IdentityDbContext<IdentityUser> is a DbContext that includes all the necessary tables and configurations for ASP.NET Core Identity with IdentityUser as the user entity

{
    public HotelProjectDbContext(DbContextOptions<HotelProjectDbContext> options) : base(options)

    {
    }

 

    public DbSet<Country> Countries { get; set; } 
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<ApiKey> ApiKeys { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Entity<ApiKey>(b => {
            b.HasIndex(k => k.Key).IsUnique();
            });
    }
}
