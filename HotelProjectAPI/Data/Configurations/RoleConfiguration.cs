using HotelProject.Api.Common.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelProjectAPI.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{

    // Seed initial roles into the database
    // This method is called during the model creation process to configure the IdentityRole entity
    // It adds two roles: "Administrator" and "User" with predefined IDs and concurrency stamps
    // The roles are normalized for case-insensitive comparisons
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
           new IdentityRole
           {
               Id = "c78e8f15-6a6c-4c8a-b5d1-98394b071953",
               ConcurrencyStamp = "a1b2c3d4-e5f6-4a5b-8c9d-1234567890ab",
               Name = RoleNames.Administrator,
               NormalizedName = RoleNames.Administrator.ToUpper()
           },
           new IdentityRole
           {
               Id = "36aac992-72ff-4527-9008-52e7c145ca39",
               ConcurrencyStamp = "f6e5d4c3-b2a1-4b5c-9d8e-0987654321ba",
               Name = RoleNames.User,
               NormalizedName = RoleNames.User.ToUpper()
           },
           new IdentityRole
           {
               Id = "36aac992-6a6c-4527-4c8a-52e7c145ca39",
               ConcurrencyStamp = "f6e5d4c3-b2a1-4b5c-9d8e-0987654321ba",
               Name = RoleNames.HotelAdmin,
               NormalizedName = RoleNames.HotelAdmin.ToUpper()
           }
       );
    }
}