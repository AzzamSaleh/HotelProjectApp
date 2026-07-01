using HotelProjectAPI.Constants;
using HotelProjectAPI.Contracts;
using HotelProjectAPI.Data;
using HotelProjectAPI.Handlers;
using HotelProjectAPI.MappingProfiles;
using HotelProjectAPI.Models;
using HotelProjectAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the IoC container.

// Configure DbContext with SQL Server connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("HotelProjectDbConnectionString");

builder.Services.AddDbContext<HotelProjectDbContext>(options =>
    options.UseSqlServer(connectionString));



builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddScoped<IHotelsService, HotelsService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IApiKeyValidatorService, ApiKeyValidatorService>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<HotelMappingProfile>();
    cfg.AddProfile<CountryMappingProfile>();
});

// Configure Identity services
//builder.Services.AddIdentityCore<ApplicationUser>(option => { })//IdentityUser, the Default class that represents a user in the Identity system
//    .AddRoles<IdentityRole>()//IdentityRole, the Default class that represents a role in the Identity system
//    .AddEntityFrameworkStores<HotelProjectDbContext>();//Use the HotelProjectDbContext as the store for Identity

// Configure Identity API endpoints
// Customize the Identity API endpoints for user management, registration, and authentication
builder.Services.AddIdentityApiEndpoints<ApplicationUser>(option => { })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<HotelProjectDbContext>();



//Implementing the Basic Authentication scheme for the Identity API endpoints. 
//This will allow clients to authenticate using a username and password when accessing the Identity API endpoints.
builder.Services.AddAuthentication(options =>
{
    //options.DefaultAuthenticateScheme = AuthenticationDefaults.BasicScheme;
    //options.DefaultChallengeScheme = AuthenticationDefaults.BasicScheme;
    //options.DefaultAuthenticateScheme = AuthenticationDefaults.ApiKeyScheme;
    // options.DefaultChallengeScheme = AuthenticationDefaults.ApiKeyScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options => 
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"] ?? string.Empty)),
        ClockSkew = TimeSpan.Zero // Default is 5 minutes
    };
})
.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(AuthenticationDefaults.BasicScheme, _ => { })
.AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(AuthenticationDefaults.ApiKeyScheme, _ => { });




builder.Services.AddAuthorization();

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

//middleware to handle Identity API endpoints
// This middleware will handle requests to the Identity API endpoints, such as registration, login, and user management
app.MapGroup("api/defaultauth").MapIdentityApi<ApplicationUser>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
