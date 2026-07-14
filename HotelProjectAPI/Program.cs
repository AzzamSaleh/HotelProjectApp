
using HotelProject.Api.Common.Constants;
using HotelProject.Api.Common.Models;
using HotelProject.Api.Domain;
using HotelProject.Api.Domain.Models;
using HotelProjectAPI.Contracts;
using HotelProjectAPI.Handlers;
using HotelProjectAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the IoC container.

// Configure DbContext with SQL Server connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("HotelProjectDbConnectionString");

builder.Services.AddDbContext<HotelProjectDbContext>(options =>
    options.UseSqlServer(connectionString));


//register services for dependency injection in ioC container, so that they can be injected into controllers or other services where needed
builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddScoped<IHotelsService, HotelsService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IApiKeyValidatorService, ApiKeyValidatorService>();


//builder.Services.AddAutoMapper(cfg =>
//{
//    cfg.AddProfile<HotelMappingProfile>();
//    cfg.AddProfile<CountryMappingProfile>();
//});

builder.Services.AddAutoMapper(cfg => { },Assembly.GetExecutingAssembly());



// Configure Identity services
//builder.Services.AddIdentityCore<ApplicationUser>(option => { })//IdentityUser, the Default class that represents a user in the Identity system
//    .AddRoles<IdentityRole>()//IdentityRole, the Default class that represents a role in the Identity system
//    .AddEntityFrameworkStores<HotelProjectDbContext>();//Use the HotelProjectDbContext as the store for Identity

// Configure Identity API endpoints
// Customize the Identity API endpoints for user management, registration, and authentication
builder.Services.AddIdentityApiEndpoints<ApplicationUser>(option => { })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<HotelProjectDbContext>();



// Add HttpContextAccessor to access the current HTTP context in services
// This is useful for accessing user information, request details, etc., in services like BookingService
// It allows services to access the current HTTP context, which is necessary for operations like retrieving the current user's ID for bookings.
builder.Services.AddHttpContextAccessor();



//bind the JwtSettings section from the configuration to the JwtSettings class, so that it can be injected into services or controllers where needed
//bind our project appsettings.json to our jwt model
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>() ?? new JwtSettings();
if (string.IsNullOrWhiteSpace(jwtSettings.Key))
{
    throw new InvalidOperationException("JwtSettings:Key is not configured.");
}





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
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options => 
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
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
